CREATE TABLE tbl_identificadores (
	id SERIAL CONSTRAINT pk_id_identificadores PRIMARY KEY,
	ident_type integer NOT NULL,
	identifier varchar(20) NOT NULL,
	UNIQUE(identifier)
);

CREATE TABLE tbl_editoras (
	id SERIAL CONSTRAINT pk_id_editor PRIMARY KEY,
	name varchar(35) UNIQUE NOT NULL
);

CREATE TABLE tbl_generos (
	id SERIAL CONSTRAINT pk_id_gender PRIMARY KEY,
	name varchar(40) NOT NULL,
	UNIQUE(name)
);

CREATE TABLE tbl_autores (
	id SERIAL CONSTRAINT pk_id_author PRIMARY KEY,
	name varchar(30) NOT NULL, 
	last_name varchar(40) NOT NULL,
	birth_date date
);

CREATE TABLE tbl_livros (
	id SERIAL CONSTRAINT pk_id_livro PRIMARY KEY,
	name varchar(50) NOT NULL,
	price money,
	pub_date date NOT NULL,
 	editor_id integer NOT NULL,
 	gender_id integer NOT NULL,
 	identifier_id integer NOT NULL,
	UNIQUE(identifier_id),
	FOREIGN KEY (editor_id) REFERENCES tbl_editoras (id) ON DELETE CASCADE,
	FOREIGN KEY (gender_id) REFERENCES tbl_generos (id) ON DELETE CASCADE,
	FOREIGN KEY (identifier_id) REFERENCES tbl_identificadores (id) ON DELETE CASCADE
);

CREATE TABLE tbl_autores_livros (
	book_id integer NOT NULL,
	author_id integer NOT NULL,
	PRIMARY KEY (book_id, author_id),
	FOREIGN KEY (book_id) REFERENCES tbl_livros (id) ON DELETE CASCADE,
	FOREIGN KEY (author_id) REFERENCES tbl_autores (id) ON DELETE CASCADE
);

