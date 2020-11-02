module Auditing
open System.IO
open System.Text
open Domain
open Operations

let auditAs
    (operationName: string) (audit: Account -> string -> unit)
    (operation: decimal -> Account -> Account) (amount: decimal) (account: Account) =
        let totalAccount = operation amount account 
        audit totalAccount operationName
    
let fileSystemAudit account message =
    let entry = sprintf "%s account: %O with owner: %s current balance: %f.\n" message account.Id account.Owner.Name account.Balance
    File.AppendAllLines(@"./test.log", [ entry ])
let console account message =
    printfn "%s account: %O with owner: %s current balance: %f.\n" message account.Id account.Owner.Name account.Balance

let withdrawWithConsoleAudit =
    withdraw |> auditAs "Withdraw from" console
let depositWithConsoleAudit =
    deposit |> auditAs "Deposit to" console
    
let withdrawWithFileAudit =
    withdraw |> auditAs "Withdraw from" fileSystemAudit
let depositWithFileAudit =
    deposit |> auditAs "Deposit to" fileSystemAudit 