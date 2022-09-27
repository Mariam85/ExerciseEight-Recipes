## Db Schema
Tables:
- recipe


	| Attribute | Type | Constraint | Nullable |
	| --- | --- | --- | --- |
	| id | int32 | Primary Key , Identity | No |
	| title | varchar(100) | | No |
  | is_active | bool | default true | No |
- ingredient
	| Attribute | Type | Constraint | Nullable |
	| --- | --- | --- | --- |
	| recipe_id | int32 | Composite Primary Key, Foreign references recipe.id | No |
	| component | varchar(1000) | Composite Primary Key | No |
- instruction
	| Attribute | Type | Constraint | Nullable |
	| --- | --- | --- | --- |
	| recipe_id | int32 | Composite Primary Key, Foreign references recipe.id | No |
	| step | varchar(1000) | Composite Primary Key | No |  
- category
	| Attribute | Type | Constraint | Nullable |
	| --- | --- | --- | --- |
	| name | varchar(100) | Primary Key | No |
  | is_active | bool | default true | No | 
- recipe_category
	| Attribute | Type | Constraint | Nullable |
	| --- | --- | --- | --- |
	| recipe_id | int32 | Composite Primary Key, Foreign references recipe.id | No |
	| category_name | varchar(100) | Composite Primary Key, Foreign references category.name | No |    
- user
	| Attribute | Type | Constraint | Nullable |
	| --- | --- | --- | --- |
	| username | varchar(100) | Primary Key | No
	| password | varchar(100) | | No |
	| refresh_token | text | | Yes



Relationships:

| Type | Tables involved 
| --- | --- |
| Many to Many | Recipes to Categories | 
