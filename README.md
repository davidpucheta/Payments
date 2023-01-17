# Payments
Payments API 


Clone or download repo and open project in Visual Studio or VSCode.
- Build Solution
-  Run Migrations
    - In Visual Studio, you can use the Package Manager Console to apply pending migrations to the database:
                <br>
                <code>PM&gt; Update-Database</code>                
    - Alternatively, you can apply pending migrations from a command prompt at your project directory:
                <br>
                <code>&gt; dotnet ef database update</code>


Run the application and use Swagger or a REST client like Postman.

Endpoints:
- <code>Card/GetBalance</code> Get the balance for a given card number.
- <code>Card/Create</code> Create a new card with the given cardNumber and balance.
- <code>Card/Pay</code> Make a payment with the given card number and amount.

Credentials, add Basic authorization headers to request:

- UserName: ApiUser
- Password: 123456
