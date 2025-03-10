namespace PseConnector.Data.Endpoints.Rce

open FSharp.Data
open System

type CmbpTpData =
    {
        Cmbp: Option<decimal>
        TypRezerwy: String
        Udtczas: DateTime
    }

module CmbpTpReader =
    let [<Literal>] ResolutionFolder = __SOURCE_DIRECTORY__
    type DataProvider = JsonProvider<"./CmbpTp.json", ResolutionFolder=ResolutionFolder>
    let data(textToParse: String): Result<CmbpTpData[], String> =
        try
            let dataArray = DataProvider.Parse(textToParse).Value
            dataArray |> Array.map(fun data ->
            {
                Cmbp = data.Cmbp
                Udtczas = data.Udtczas
                TypRezerwy = data.TypRezerwy
            }) |> Result.Ok
        with
        | e -> e.Message |> Result.Error