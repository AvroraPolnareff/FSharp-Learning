module Auditing
open System.IO
open System.Text
open Domain
open Operations

let auditAs
    (operationName: string) (audit: Account -> string -> unit)
    (operation: Account -> decimal -> Account) (amount: decimal) (account: Account) =
        let totalAccount = operation account amount
        audit totalAccount operationName
    
let fileSystemAudit account message =
    let entry = sprintf "%s account: %O with owner: %s current balance: %f.\n" message account.Id account.Owner.Name account.Balance
    File.WriteAllText(@"./test.log", entry, Encoding.UTF8)
let console account message =
    printfn "%s account: %O with owner: %s current balance: %f.\n" message account.Id account.Owner.Name account.Balance

let withdrawWithConsoleAudit =
    auditAs "Withdraw from" console withdraw
let depositWithConsoleAudit =
    auditAs "Deposit to" console deposit
    
let withdrawWithFileAudit =
    auditAs "Withdraw from" fileSystemAudit withdraw
let depositWithFileAudit =
    auditAs "Deposit to" fileSystemAudit deposit