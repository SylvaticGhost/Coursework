CREATE TABLE user_schema.user_account
(
    id UUID PRIMARY KEY,
    first_name VARCHAR(30) NOT NULL,
    last_name VARCHAR(30),
    email VARCHAR(50) NOT NULL,
    phone_number VARCHAR(15) NOT NULL,
    date_of_birth TIMESTAMP(0) WITHOUT TIME ZONE NOT NULL,
    password_hash BYTEA NOT NULL,
    password_salt BYTEA NOT NULL,
    created_at TIMESTAMP NOT NULL
);