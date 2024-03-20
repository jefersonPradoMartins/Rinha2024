# Subimissão para Rinha de Backend, segunda Edição: 2024/Q1 - Controle de concorrência.

 Submissão feita com:
- ``nginx`` como load balancer
- ``postgres`` como banco de dados
- ``.Net 8`` para API + ``EntityFramework`` e ``Npgsql``



## Arquitetura do projeto. 
````mermaid
flowchart TD
    G(Stress Test - Gatling) -.-> LB(Load Balancer / porta 9999)
    subgraph Sua Aplicação
        LB -.-> API1(API - instância 01)
        LB -.-> API2(API - instância 02)
        API1 -.-> Db[(Database)]
        API2 -.-> Db[(Database)]
    end
````

