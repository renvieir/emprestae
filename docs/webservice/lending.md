**Create Lending**
POST $base_url/emprestimo/
Required Fields: idamizade, idobjeto, dtemprestimo, dtdevolucao

**Retrieve Lending**
GET $base_url/emprestimo/:id

**Update Lending**
PUT $base_url/emprestimo/:id
Editable Fields: idamizade, idobjeto, dtemprestimo, dtdevolucao, devolvido

**Delete Lending**
DELETE  $base_url/emprestimo/:id