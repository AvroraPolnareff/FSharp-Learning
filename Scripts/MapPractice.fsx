

open System
open System.IO
let directories =
    Directory.EnumerateDirectories(@"/usr/share")
    
let foldersAge =
    directories
        |> Seq.map (fun path -> DirectoryInfo(path) )
        |> Seq.map (fun info -> (info.Name, info.CreationTimeUtc))
        |> Map.ofSeq
        |> Map.map (fun _ creationDate -> (DateTime.Now - creationDate).Days)
        |> Map.toList
        |> List.sortByDescending (fun (_, days) -> days)

printfn "%A" foldersAge