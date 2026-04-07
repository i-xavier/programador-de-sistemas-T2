CREATE DATABASE consertodolele;

USE consertodolele;

CREATE TABLE pecascomputador(
	id INT PRIMARY KEY AUTO_INCREMENT, 
    nomepeca VARCHAR(100) NOT NULL, 
    quantidade INT NOT NULL, 
    valor FLOAT NOT NULL, 
    fornecedor VARCHAR(32) NOT NULL, 
    telefone VARCHAR(11) NOT NULL, 
    ruafornecedor VARCHAR(100), 
    numerorua INT NOT NULL, 
    cepfornecedor VARCHAR(9) NOT NULL);

INSERT INTO pecascomputador (nomepeca, quantidade, valor, fornecedor, telefone, ruafornecedor, numerorua, cepfornecedor) 
VALUES ('Memória RAM 8GB', 15, 220.00, 'TechMemory', '11988776655', 'Rua da Tecnologia', 100, '01010-000'), 
('HD 1TB', 25, 310.00, 'Armazém Digital', '11997654321', 'Av. Central', 450, '02020-000'), 
('SSD 480GB', 40, 380.00, 'FastStorage', '11999887766', 'Rua Velocidade', 88, '03030-000'), 
('Placa Mãe ASUS', 10, 750.00, 'InfoPlacas', '11991234567', 'Av. das Peças', 900, '04040-000'), 
('Fonte 500W', 30, 260.00, 'PowerTech', '11993456789', 'Rua Energia', 77, '05050-000'),
('Processador i5', 8, 1250.00, 'CPU Brasil', '11994561234', 'Av. Intel', 123, '06060-000'),
('Placa de Vídeo GTX1660', 5, 1800.00, 'GameStore', '11995678901', 'Rua Gamer', 321, '07070-000'), 
('Teclado Mecânico', 50 , 350.00, 'Periféricos SP', '11996789012', 'Av dos Acessórios', 55, '08080-000'),
('Mouse Óptico', 60, 120.00, 'Periféricos SP', '11996789012', 'Av dos Acessórios', 55, '08080-000'),
('Cooler para CPU', 22, 190.00, 'RefrigTech', '11997890123', 'Rua do Resfriamento', 9, '09090-000');


SELECT * FROM pecascomputador;

SELECT fornecedor, telefone FROM pecascomputador;

SELECT * FROM pecascomputador WHERE quantidade > 20;

SELECT * FROM pecascomputador 
WHERE quantidade = 60 AND fornecedor = 'Periféricos SP';

SELECT * FROM pecascomputador 
WHERE quantidade = 40 OR fornecedor = 'Periféricos SP';

SELECT * FROM pecascomputador 
WHERE ruafornecedor LIKE '%s';

SELECT * FROM pecascomputador
ORDER BY valor ASC;

SELECT * FROM pecascomputador
ORDER BY valor DESC;

SELECT * FROM pecascomputador
LIMIT 4;


CREATE TABLE pedidocliente(
idcliente INT PRIMARY KEY AUTO_INCREMENT, 
nome VARCHAR(32), 
sobrenome VARCHAR(50), 
email VARCHAR(100), 
telefone VARCHAR(11), 
id_produto INT, 
FOREIGN KEY(id_produto) REFERENCES pecascomputador(id)
);

INSERT INTO pedidocliente(nome, sobrenome, email, telefone, id_produto)
VALUES('Ana', 'Silva', 'ana.silva@email.com', '11988887777', 3);

INSERT INTO pedidocliente(nome, sobrenome, email, telefone, id_produto)
VALUES('Carlos', 'Souza', 'carlos.souza@email.com', '11999998888', 5);

INSERT INTO pedidocliente(nome, sobrenome, email, telefone, id_produto)
VALUES('Mariana', 'Oliveira', 'mariana.oliveira@email.com', '11977776666', 2);

INSERT INTO pedidocliente(nome, sobrenome, email, telefone, id_produto)
VALUES('João', 'Pereira', 'joao.pereira@email.com', '11966665555', 1);

INSERT INTO pedidocliente(nome, sobrenome, email, telefone, id_produto)
VALUES('Fernanda', 'Costa', 'fernanda.costa@email.com', '11955554444', 4);


SELECT p.nomepeca, p.valor, c.nome FROM pecascomputador p INNER JOIN pedidocliente c ON p.id = c.idcliente WHERE c.nome = 'Ana';

SELECT 
    a.nomepeca, 
    a.valor, 
    b.nome
FROM pedidocliente b
INNER JOIN pecascomputador a 
ON b.id_produto = a.id 
ORDER BY 
	b.nome DESC;

CREATE USER 'novo_usuario'@'localhost' IDENTIFIED BY 'senha123';

GRANT SELECT ON consertodolele.* TO 'novo_usuario'@'localhost';

REVOKE SELECT ON consertodolele.* FROM 'novo_usuario'@'localhost';

DROP USER 'novo_usuario'@'localhost';