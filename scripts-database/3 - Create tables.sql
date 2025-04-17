BEGIN;


ALTER TABLE IF EXISTS public.rentals DROP CONSTRAINT IF EXISTS fk_deliveryman;

ALTER TABLE IF EXISTS public.rentals DROP CONSTRAINT IF EXISTS fk_motorcycle_rental;

DROP TABLE IF EXISTS public.deliverymen;

CREATE TABLE IF NOT EXISTS public.deliverymen
(
    id character varying COLLATE pg_catalog."default" NOT NULL,
    name text COLLATE pg_catalog."default" NOT NULL,
    document character varying(14) COLLATE pg_catalog."default" NOT NULL,
    birth_date date NOT NULL,
    drivers_license character varying(11) COLLATE pg_catalog."default" NOT NULL,
    drivers_license_category character varying(3) COLLATE pg_catalog."default" NOT NULL,
    created_date timestamp without time zone NOT NULL DEFAULT now(),
    updated_date timestamp without time zone,
    CONSTRAINT pk_deliveryman PRIMARY KEY (id),
    CONSTRAINT unique_deliveryman_document UNIQUE (document),
    CONSTRAINT unique_drivers_license UNIQUE (drivers_license)
);

COMMENT ON COLUMN public.deliverymen.document
    IS 'CNPJ';

DROP TABLE IF EXISTS public.motorcycles;

CREATE TABLE IF NOT EXISTS public.motorcycles
(
    id character varying COLLATE pg_catalog."default" NOT NULL,
    fabrication_year smallint NOT NULL,
    model character varying(100) COLLATE pg_catalog."default" NOT NULL,
    plate character varying(8) COLLATE pg_catalog."default" NOT NULL,
    created_date timestamp without time zone NOT NULL DEFAULT now(),
    updated_date timestamp without time zone,
    CONSTRAINT pk_motorcycle_id PRIMARY KEY (id),
    CONSTRAINT unique_motorcycle_plate UNIQUE (plate)
);

DROP TABLE IF EXISTS public.rentals;

CREATE TABLE IF NOT EXISTS public.rentals
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    delivery_man_id character varying COLLATE pg_catalog."default" NOT NULL,
    motorcycle_id character varying COLLATE pg_catalog."default" NOT NULL,
    start_date timestamp without time zone NOT NULL,
    end_date timestamp without time zone NOT NULL,
    expected_end_date timestamp without time zone NOT NULL,
    return_date timestamp without time zone,
    plan smallint NOT NULL,
    daily_value numeric NOT NULL,
    created_date timestamp without time zone NOT NULL DEFAULT now(),
    updated_date timestamp without time zone,
    CONSTRAINT pk_rental_id PRIMARY KEY (id)
);

ALTER TABLE IF EXISTS public.rentals
    ADD CONSTRAINT fk_deliveryman FOREIGN KEY (delivery_man_id)
    REFERENCES public.deliverymen (id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION
    NOT VALID;


ALTER TABLE IF EXISTS public.rentals
    ADD CONSTRAINT fk_motorcycle_rental FOREIGN KEY (motorcycle_id)
    REFERENCES public.motorcycles (id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION
    NOT VALID;

END;

