# Sample Microservices

    docker build -t customerapi .
    docker build -t orderapi .
    docker-compose up
    docker-compose down
    docker-compose run --rm
    docker-compose -f docker-compose-azure.yml up

    ## Customer
    ### PUT
    curl --request PUT --url http://localhost:5000/api/v1.0/customer --header 'content-type: application/json' -d '{ "Id": "9f35b48d-cb87-4783-bfdb-21e36012930a", "FirstName":"Amin2", "LastName":"Ziagham2", "Email":"amin.ziagham@gmail.com", "BirthDate":"2012-04-23T18:25:43.511Z"}'

    ### POST
    curl --request POST --url http://localhost:5000/api/v1.0/customer --header 'content-type: application/json' -d '{ "FirstName":"Johnny", "LastName":"Depp", "Email":"johnny.depp@gmail.com", "BirthDate":"1963-06-09T18:25:43.511Z"}'

    ### GET
    curl --request GET --url http://localhost:5000/api/v1.0/customer



    ## Order
    ### PUT
    curl --request PUT --url http://localhost:6000/api/v1.0/order/pay/c34a25d8-e786-4e00-9b70-6acf2e6187ac --header 'content-type: application/json' -d {}

    ### POST
    curl --request POST --url 'http://localhost:6000/api/v1.0/order' --header 'content-type: application/json' -d '{ "CustomerGuid": "9264f09c-83f8-4bef-a00b-00d673bb274c", "CustomerFullName": "Johnny Depp"}'

    ### GET
    curl --request GET --url http://localhost:6000/api/v1.0/order