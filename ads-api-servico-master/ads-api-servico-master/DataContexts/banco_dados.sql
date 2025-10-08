DROP DATABASE IF EXISTS suporte_db;
CREATE DATABASE suporte_db;
USE suporte_db;

CREATE TABLE prioridade (
id_pri INT PRIMARY KEY AUTO_INCREMENT,
nome_pri VARCHAR(50) NOT NULL
);
CREATE TABLE chamado (
	id_cha INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	titulo_cha VARCHAR(255) NOT NULL,
	descricao_cha TEXT NOT NULL,
	data_abertura_cha DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP(),
	data_fechamento_cha DATETIME NULL,
	situacao_cha VARCHAR(50) NOT NULL DEFAULT 'Aberto',
    id_pri_fk INT NOT NULL,
    FOREIGN KEY (id_pri_fk) REFERENCES prioridade(id_pri)
);

SELECT * FROM chamado;
SELECT * FROM prioridade;
INSERT INTO  prioridade(nome_pri) VALUES('Baixa'), ('Média'), ('Alta'),('Crítica');

INSERT INTO chamado (id_cha, titulo_cha, descricao_cha, data_abertura_cha, data_fechamento_cha, situacao_cha,id_pri_fk)
VALUES
(1, 'Erro no sistema de login', 'Usuário não consegue acessar com credenciais válidas.', '2025-09-01 22:10:00', '2025-09-03 10:05:00', 'Fechado',1),
(2, 'Problema na impressão', 'Impressora da recepção não imprime documentos.', '2025-09-02 09:25:01', NULL, 'Aberto',2),
(3, 'Lentidão no sistema', 'Sistema de vendas apresentando lentidão no horário de pico.', '2025-09-04 14:32:00', NULL, 'Em andamento',3),
(4, 'Atualização de software', 'Solicitação para atualização do ERP para a versão mais recente.', '2025-09-05 16:45:01', '2025-09-06 14:02:00', 'Fechado',4),
(5, 'Acesso ao banco de dados', 'Necessidade de liberar acesso ao banco para o setor financeiro.', '2025-09-07 17:59:59', NULL, 'Aberto',4);