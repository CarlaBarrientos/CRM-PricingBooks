# ${Pricing Books}
TODO: Since the company accommodates its products in relation to campaigns for a year; Products have different prices, therefore it is required:
Basic Operations (CRUD) of the Price Lists
O Name of the Price List (2020 Price List)
O Description
O Enable or disable Price List
▪ ONLY ONE ACTIVE AT THE SAME TIME
Or Products List with their prices.
(Product1 + Price, Product2 + Price, Product3 + Price)
Basic Operations (CRUD) for the product list:
O Product Code (ONLY) - See example of Products module => SOCCER-001
O Fixed Price => 200 bs.
O Promotion Price is calculated based on the fixed price according to the active campaign:
▪ If it is Christmas: 5% <=> XMAS => Endpoint
▪ If it is Summer: 20%
▪ If it is BLACK FRIDAY: 25%
▪ If there is no active campaign there is no promotional price





Grupo 4: 
● Que los endpoints sean parte del API GATEWAY (Gabriel Acosta, Alejandra Quelali)
    
    o /api-crm/pricing-books =&gt; HTTP/GET: http://localhost:XXXX/pricing-books =&gt;
    send/return same values

    o /api-crm/pricing-books/{id}

    o etc

● Que los datos sean persistidos en una base de datos como: JSON file (configurable) (Vanessa Bustillos)
    
    o c:\websites\crm\pricing-books.json

● Use Backing Service: Campaigns to calculate the price based on active campaign (Diego Rosazza)

● Que la aplicación PRICING BOOKS contenga manejo de errores (Ricardo Fernandez)

● OPCIONAL: Que la aplicación PRICING BOOKS contenga registro logs
● OPCIONAL: Crear una interfaz gráfica sencilla usando VUE.JS
    o Debe existir dos vistas:
        ▪ una para el listado de Pricing Books
        ▪ otra el detalle de los Pricing Books más sus listados de Produtos
    o El habilitar/deshabilitar pricing books debe ser un switch button

## Credits

