namespace Domain

type Customer =
    { Name: string }
 
type Account =
    { Id: System.Guid
      Balance: decimal
      Owner: Customer }
    