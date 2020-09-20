CREATE DATABASE db_sidedesk

CREATE TABLE `db_sidedesk`.`advancedPro` (
	`advp_id` INT NOT NULL AUTO_INCREMENT ,
	`advp_mac_address` VARCHAR(256) NULL ,
	`advp_license` VARCHAR(256) NULL ,
	`advp_user` VARCHAR(256) NULL ,
	PRIMARY KEY (`advp_id`))
	ENGINE = MyISAM;

//status users(active, inactive);

CREATE TABLE `db_sidedesk`.`tb_user` ( 
	`user_id` INT NOT NULL AUTO_INCREMENT , 
	`user_name` VARCHAR(256) NOT NULL ,
	`user_nickname` VARCHAR(256) NOT NULL ,
	`user_email` VARCHAR(256) NOT NULL ,
	`user_office` VARCHAR(256) NOT NULL ,
	`user_contacto` VARCHAR(64) NOT NULL , 
	`user_password` VARCHAR(256) NOT NULL , 
	`user_privilege` VARCHAR(64) NOT NULL,
	`user_AccessLevel` INT NOT NULL ,
	`user_createDate` VARCHAR(64) NOT NULL,
	`user_status` VARCHAR(64) NOT NULL , 
	PRIMARY KEY (`user_id`)) 
	ENGINE = MyISAM;

CREATE TABLE `db_sidedesk`.`tb_user_login`(
	`login_id` INT NOT NULL AUTO_INCREMENT ,
	`login_user_id` int not null, 	
	`login_user_name` varchar(100) not null,
	`login_initial_state` varchar(32) not null,
	`login_final_state` varchar(32) not null,
	`login_date` varchar(32) not null,
	PRIMARY KEY (`login_id`),
	FOREIGN KEY (`login_user_id`) REFERENCES tb_user (`user_id`) ON DELETE CASCADE
);

CREATE TABLE `db_sidedesk`.`tb_login_attempt`(
	`attempt_id` INT NOT NULL AUTO_INCREMENT,
	`attempt_name_used` varchar(100) not null, 	
	`attempt_password_used` varchar(256) not null,
	`attempt_date` varchar(32) not null,
	PRIMARY KEY (`attempt_id`)
);

CREATE TABLE `db_sidedesk`.`tb_client` ( 
	`client_id` INT NOT NULL AUTO_INCREMENT , 
	`client_office_id` INT NOT NULL , 
	`client_user_id` INT NOT NULL , 
	`client_nome` VARCHAR(64) NOT NULL ,
	`client_dtAniversario` VARCHAR(64) NOT NULL , 
	`client_contacto1` VARCHAR(64) NOT NULL , 
	`client_contacto2` VARCHAR(64) NULL , 
	`client_provincia` VARCHAR(64) NULL , 
	`client_obs` VARCHAR(512) NULL , 
	`client_dt` VARCHAR(64) NOT NULL ,
	 PRIMARY KEY (`client_id`))
	 ENGINE = MyISAM;

CREATE TABLE `db_sidedesk`.`tb_encomenda` ( 
	`enc_id` INT NOT NULL AUTO_INCREMENT , 
	`enc_office_id` INT NOT NULL , 
	`enc_user_id` INT NOT NULL , 
	`enc_client_id` INT NOT NULL , 
	`enc_prod_id` INT NOT NULL ,
	`enc_tipo_pagamento` VARCHAR(256) NULL ,
	`enc_montante` DOUBLE NOT NULL , 
	`enc_desconto` DOUBLE NOT NULL , 
	`enc_transporte` DOUBLE NOT NULL , 
	`enc_total` DOUBLE NOT NULL ,
	`enc_total_global` DOUBLE NOT NULL ,
	`enc_saldo` DOUBLE NOT NULL ,
	`enc_receptor` VARCHAR(64) NULL , 
	`enc_recp_contacto` BIGINT NULL , 
	`enc_tipo_transporte` VARCHAR(256) NULL , 
	`enc_estado` VARCHAR(32) NOT NULL , 
	`enc_dataEF` VARCHAR(64) NOT NULL ,
	`enc_dataET` VARCHAR(64) NOT NULL ,
	PRIMARY KEY (`enc_id`)) ENGINE = MyISAM;

CREATE TABLE `db_sidedesk`.`tb_encomenda_detalhe` ( 
	`dt_id` INT NOT NULL AUTO_INCREMENT , 
	`dt_enc_id` INT NOT NULL ,
	`dt_prod_id` INT NOT NULL , 
	`dt_serv_id` INT NOT NULL ,
	`dt_quant` DOUBLE NOT NULL ,
	`dt_preco` DOUBLE NOT NULL , 
	PRIMARY KEY (`dt_id`)) ENGINE = MyISAM;

CREATE TABLE `db_sidedesk`.`tb_encomenta_movimento` ( 
	`enc_mov_id` INT NOT NULL AUTO_INCREMENT , 
	`enc_mov_estado` VARCHAR(64) NULL , 
	`enc_mov_funcAtendente` VARCHAR(64) NULL ,  
	`enc_mov_data` VARCHAR(64) NOT NULL , 
	`enc_enc_mov_formPagamento` VARCHAR(256) NULL , 
	`enc_mov_enc_id` INT NOT NULL , 
	`enc_mov_montante` DOUBLE NOT NULL , 
	PRIMARY KEY (`enc_mov_id`)) ENGINE = MyISAM;


CREATE TABLE `db_sidedesk`.`tb_produto` ( 
	`prod_id` INT NOT NULL AUTO_INCREMENT , 
	`prod_cat_id` INT NOT NULL, 
	`prod_subcat_id` INT NOT NULL , 
	`prod_nome` VARCHAR(256) NOT NULL , 
	`prod_precoCompra` Decimal(10,2) NOT NULL , 
	`prod_precoVenda` Decimal(10,2) NOT NULL , 
	`prod_data` VARCHAR(64) NOT NULL , 
	`prod_office_id` INT NOT NULL , 
	`prod_user_id` INT NOT NULL , 
	PRIMARY KEY (`prod_id`)) ENGINE = MyISAM;

CREATE TABLE `db_sidedesk`.`tb_servicos` ( 
	`serv_id` INT NOT NULL AUTO_INCREMENT , 
	`serv_cat_id` INT NOT NULL , 
	`serv_subcat_id` INT NOT NULL ,
	`serv_prod_id` INT NOT NULL , 
	`serv_nome` VARCHAR(256) NOT NULL , 
	`serv_preco` Decimal(10,2) NOT NULL , 
	`serv_data` VARCHAR(64) NOT NULL , 
	`serv_office_id` INT NOT NULL , 
	`serv_user_id` INT NOT NULL , 
	PRIMARY KEY (`serv_id`)) ENGINE = MyISAM;

CREATE TABLE `db_sidedesk`.`tb_stock` ( 
	`stk_prod_id` INT NOT NULL,
	`stk_cat_id` INT NOT NULL,
	`stk_subcat_id` INT NOT NULL,
	`stk_prod_nome` VARCHAR(256) NOT NULL,
	`stk_office_id` INT NOT NULL, 
	`stk_qtd_disponivel` INT NOT NULL,
	`stk_qtd_danificada` INT NOT NULL) ENGINE = MyISAM;

CREATE TABLE `db_sidedesk`.`tb_office` ( 
	`off_id` INT NOT NULL, 
	`off_empresa` VARCHAR(256) NOT NULL , 
	`off_escritorio` VARCHAR(256) NOT NULL , 
	`off_cidade` VARCHAR(256) NOT NULL , 
	`off_contacto` BIGINT NOT NULL , 
	`off_email` VARCHAR(256) NOT NULL , 
	`off_nuit` BIGINT NOT NULL , 
	`off_logotipo` VARCHAR(64) NOT NULL , 
	`off_data` VARCHAR(64) NOT NULL 
	) ENGINE = MyISAM;

CREATE TABLE `db_sidedesk`.`tb_permissao` ( 
	`perm_id` INT NOT NULL AUTO_INCREMENT , 
	`perm_user_id` INT NOT NULL , 
	`perm_form` VARCHAR(256) NOT NULL , 
	`perm_check` BOOLEAN NOT NULL , 
	PRIMARY KEY (`perm_id`)) ENGINE = MyISAM;

CREATE TABLE `db_sidedesk`.`tb_categoria` ( 
	`cat_id` INT NOT NULL AUTO_INCREMENT , 
	`cat_nome` VARCHAR(256) NOT NULL , 
	PRIMARY KEY (`cat_id`)) ENGINE = MyISAM;

CREATE TABLE `db_sidedesk`.`tb_subcategoria` ( 
	`subcat_id` INT NOT NULL AUTO_INCREMENT , 
	`subcat_cat_id` INT NOT NULL , 
	`subcat_nome` VARCHAR(256) NOT NULL , 
	PRIMARY KEY (`subcat_id`),
	FOREIGN KEY (`subcat_cat_id`) REFERENCES tb_categoria (`cat_id`) ON DELETE CASCADE
	)ENGINE = MyISAM;
