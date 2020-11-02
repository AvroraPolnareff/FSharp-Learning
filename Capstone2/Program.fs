// Learn more about F# at http://fsharp.org

open System
open Domain
open Auditing
open Operations

let readln =
    System.Console.ReadLine
let toDecimal s =
    Decimal.Parse(s)
    
    

[<EntryPoint>]
let main argv =
    printfn "Enter your name!"
    let mutable account = newAccount { Name = readln() }
    
    while true do
        printfn "Press 1 to deposit or 2 to withdraw; X to exit."
        let action = readln()
        if action = "X" then Environment.Exit 0
            
        printf "Enter amount: "
        let amount = readln() |> toDecimal
        account <-
            if action = "1" then depositWithConsoleAudit amount account; account |> deposit amount
            elif action = "2" then withdrawWithConsoleAudit amount account; account |> withdraw amount
            else account
    
    0 // return an integer exit code
