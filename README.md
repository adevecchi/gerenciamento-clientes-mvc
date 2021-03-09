## Descrição:

Projeto MVC C# (.NET Framework), utilizando janela modal.

Gerenciamento de dados de Clientes: Cadastro, Consulta, Edição e Exclusão com uso de Procedures.

O script T-SQL **script.sql** cria o bando de dados **dbteste** com a tabela **Cliente** e as procedures para **Select, Insert, Update e Delete**.

Também gera 5000 registros na tabela **Cliente** com o formato mostrado abaixo:

```text
Cliente ('Teste1','TipoCliente1','NomeContato1','1111-1111','Cidade1','Bairro1','Rua Teste1','10/03/2015','13/04/2015')
Cliente ('Teste2','TipoCliente2','NomeContato2','1111-1111','Cidade2','Bairro2','Rua Teste2','11/03/2015','14/04/2015')
...
``` 

O banco de dados se encontra no caminho **"~\WebSite\App_Data\dbteste.mdf"** da aplicação.

## Ferramentas e tecnologias usadas

* Visual Studio Community 2015
* SQL Server 2014 Express
* .NET Framework 4.5.2

## Captura de tela

![Tela Clientes](https://github.com/adevecchi/gerenciamento-clientes-mvc/blob/main/WebSite/Content/images/screenshot/cliente-index.png)

---

![Tela Clientes](https://github.com/adevecchi/gerenciamento-clientes-mvc/blob/main/WebSite/Content/images/screenshot/cliente-novo.png)

---

![Tela Clientes](https://github.com/adevecchi/gerenciamento-clientes-mvc/blob/main/WebSite/Content/images/screenshot/cliente-atualizar.png)

---

![Tela Clientes](https://github.com/adevecchi/gerenciamento-clientes-mvc/blob/main/WebSite/Content/images/screenshot/cliente-excluir.png)