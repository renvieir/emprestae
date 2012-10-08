**Create User**
POST $base_url/user/
Required Fields: name, email, password, address

**Retrieve User**
GET $base_url/user/:id

**Update User**
PUT $base_url/user/:id
Editable Fields: name, email, password

**Delete User**
DELETE  $base_url/user/:id
