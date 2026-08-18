# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is

A Blazor WebAssembly app that queries the Polish power grid operator's (PSE) public reporting API (`https://api.raporty.pse.pl/api/`) and renders the results as line charts. There is no backend server for app logic — the WASM app calls the PSE API directly from the browser, and in production is served as static files behind nginx.

## Projects

- **PseConnector.Web** — Blazor WebAssembly UI (net8.0). The actual product; contains pages, charting, and PSE API calls.
- **PseConnector.Console** — a scratch console app for exploring the PSE API (`Program.cs` is a throwaway script, not a real CLI).

**Important quirk:** `PseConnector.Web` has a `ProjectReference` to `PseConnector.Data`, but the Web project does not actually use it — the Razor pages parse PSE JSON responses themselves via `System.Net.Http.Json` / `System.Text.Json` / Newtonsoft.Json into their own C# models in `PseConnector.Web/Models/`. The F# reader modules (`RcePlnReader`, `CmbpTpReader`) are unused by the Web app today. Don't assume changes to `PseConnector.Data` affect the running app.

## Build / run / publish

```bash
dotnet restore PseConnector.sln
dotnet build PseConnector.sln
dotnet run --project PseConnector.Web
```

There are no automated tests in this repo currently.

`PseConnector.Data/PseTest.http` has example raw PSE API requests (usable with the VS Code REST Client extension or similar) — a fast way to check what a PSE endpoint actually returns before wiring up a page.

### Docker (production build)

```bash
docker build -t pseconnector .
```

Two-stage build: stage 1 publishes `PseConnector.Web` with the .NET 8 SDK, stage 2 copies the wwwroot output into `nginx:alpine` (config: [nginx.conf](nginx.conf)) which serves it on port 80 with SPA fallback (`try_files $uri $uri/ /index.html`). `.github/workflows/docker-publish.yml` builds and pushes this image to `ghcr.io/<repo>` on every push to `master`, tagged `latest` and by commit SHA.

## Architecture: adding a new PSE connector page

Each PSE endpoint (e.g. `rce-pln`, `cmbp-tp`, `cmbu-tu`, `energy-prices`) gets its own routed page under `PseConnector.Web/Pages/`, following the same shape as the existing ones (e.g. [RceConnector.razor](PseConnector.Web/Pages/RceConnector.razor)):

1. `@page` route + `PageTitle`, injects `PseHttpClientService` (thin wrapper over a pre-configured `HttpClient` pointed at the PSE base API — see [PseHttpClientService.cs](PseConnector.Web/PseHttpClientService.cs) and its registration in [Program.cs](PseConnector.Web/Program.cs)) and `IJSRuntime`.
2. Renders a shared `<PseChartInput>` component ([PseChartInput.razor](PseConnector.Web/Components/PseChartInput.razor)) for date-from/date-to/limit inputs, two-way bound via `@bind-DateFrom`/`@bind-DateTo`/`@bind-Limit`/`@bind-Error`.
3. Builds a PSE OData-style query: `{endpoint}?$first={limit}&$filter=business_date ge 'yyyy-MM-dd' and business_date le 'yyyy-MM-dd'` (see the `Endpoint`/`QueryParamBusinessDate` consts in any existing page).
4. Fetches via `PseHttpClient.GetFromJsonAsync<T>(url)` into a typed model in `PseConnector.Web/Models/`, OR (see `CmbuTuConnector.razor` for a schema-agnostic alternative) fetches into a raw `System.Text.Json.JsonElement` and dynamically discovers numeric series by property name — useful when the PSE endpoint's field set isn't fixed/known in advance.
5. Maps the result into `PseChartData`/`PseChartSeries` ([PseLineChartModels.cs](PseConnector.Web/Components/PseLineChartModels.cs)), one series per numeric field, cycling colors via `ColorUtility.CategoricalTwelveColors[i % ColorUtility.CategoricalTwelveColors.Length]`. Chart is a `<PseLineChart @ref="_chart" YAxisTitle="...">` ([PseLineChart.razor](PseConnector.Web/Components/PseLineChart.razor) — a thin custom Chart.js wrapper, not the Blazor.Bootstrap chart component), updated via `_chart.UpdateAsync(chartData)` after fetch; pass `IsLoading="_isLoading"` (toggled around the fetch) so the component shows its built-in loading spinner. A second Y axis is available via `Y1AxisTitle` + per-series `AxisId = "y1"` when comparing series with different units (see [RceEbRozlCompareConnector.razor](PseConnector.Web/Pages/RceEbRozlCompareConnector.razor)).
6. A "JSON" button downloads the raw fetched result client-side via the JS interop function `downloadFileFromStream` in [wwwroot/SaveFile.js](PseConnector.Web/wwwroot/SaveFile.js) (invoked through `IJSRuntime`).
7. Register the new route in **both** [NavMenu.razor](PseConnector.Web/Layout/NavMenu.razor) (sidebar link) and [Home.razor](PseConnector.Web/Pages/Home.razor) (the home page's list of pages) — always add both, not just one.

PSE API errors and JSON shape mismatches are caught per-action and surfaced through the shared `Error` string bound into `<PseChartInput>` — there's no global error boundary.
