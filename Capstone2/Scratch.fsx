open System.IO
open System.Text
type Customer =
    { Name: string }
 
type Account =
    { Id: System.Guid
      Balance: decimal
      Owner: Customer }
    
let deposit account amount =
    let difference = account.Balance + amount
    { account with Balance = difference }
    
let withdraw account amount =
   let difference = account.Balance - amount
   if difference >= 0m then
       { account with Balance = difference }
   else
       account
       
let newAccount owner =
    { Id = System.Guid.NewGuid(); Balance = 0m; Owner = owner }
    
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

