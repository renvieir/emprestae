**Create Book**
POST $base_url/livro/
Required Fields: isbn, autor, titulo, editora, edicao

**Retrieve Book**
GET $base_url/livro/:isbn

**Update Book**
PUT $base_url/livro/:isbn
Editable Fields: autor, titulo, editora, edicao

**Delete Book**
DELETE  $base_url/livro/:isbn