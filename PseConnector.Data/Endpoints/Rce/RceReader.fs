namespace PseConnector.Data.Endpoints.Rce

open FSharp.Data
open System

type RceData =
    {
        RcePln: decimal
        Udtczas: DateTime
    }

module RceReader =
    let [<Literal>] ResolutionFolder = __SOURCE_DIRECTORY__
    type RceProvider = JsonProvider<"./Rce.json", ResolutionFolder=ResolutionFolder>
    let rceData(textToParse: String) : RceData[] =
        let dataArray = RceProvider.Parse(textToParse).Value
        dataArray |> Array.map(fun data ->
        {
            RcePln = data.RcePln
            Udtczas = data.Udtczas
        })