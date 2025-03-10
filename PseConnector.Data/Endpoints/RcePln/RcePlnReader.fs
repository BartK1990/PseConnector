namespace PseConnector.Data.Endpoints.Rce

open FSharp.Data
open System

type RcePlnData =
    {
        RcePln: decimal
        Udtczas: DateTime
    }

module RcePlnReader =
    let [<Literal>] ResolutionFolder = __SOURCE_DIRECTORY__
    type DataProvider = JsonProvider<"./RcePln.json", ResolutionFolder=ResolutionFolder>
    let data(textToParse: String): Result<RcePlnData[], String> =
        try
            let dataArray = DataProvider.Parse(textToParse).Value
            dataArray |> Array.map(fun data ->
            {
                RcePln = data.RcePln
                Udtczas = data.Udtczas
            }) |> Result.Ok
        with
        | e -> e.Message |> Result.Error