**Create Game**
POST $base_url/jogo/
Required Fields: titulo, console, editora

**Retrieve Game**
GET $base_url/jogo/:id

**Update Game**
PUT $base_url/jogo/:id
Editable Fields: titulo, console, editora

**Delete Game**
DELETE  $base_url/jogo/:id