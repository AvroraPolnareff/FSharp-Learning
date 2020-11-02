module Operations
open Domain

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