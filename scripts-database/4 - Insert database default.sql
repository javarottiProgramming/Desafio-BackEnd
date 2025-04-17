
INSERT INTO public.deliverymen(id, name, document, birth_date, 
	drivers_license, drivers_license_category)
VALUES ('entregador123', 'João da Silva', '30393263000179', '1990-01-01T00:00:00Z', 
	'88960316860', 'A');

INSERT INTO public.deliverymen(id, name, document, birth_date, 
	drivers_license, drivers_license_category)
VALUES ('entregador456', 'João Lucas', '56854350000170', '1990-01-01T00:00:00Z', 
	'30375305279', 'A+B');
	
INSERT INTO public.deliverymen(id, name, document, birth_date, 
	drivers_license, drivers_license_category)
VALUES ('entregador789', 'João  Pedro', '42975305000169', '1990-01-01T00:00:00Z', 
	'08991166447', 'B');

INSERT INTO public.motorcycles(
	id, fabrication_year, model, plate)
	VALUES ('moto123', 2020, 'Mottu Sport', 'CDX-0101');


INSERT INTO public.motorcycles(
	id, fabrication_year, model, plate)
	VALUES ('moto456', 2021, 'Mottu Sport', 'CDX-0102');

INSERT INTO public.motorcycles(
	id, fabrication_year, model, plate)
	VALUES ('moto789', 2022, 'Mottu Sport', 'CDX-0103');


INSERT INTO public.rentals(
	delivery_man_id, motorcycle_id, start_date, 
	end_date, expected_end_date, plan, daily_value, created_date)
VALUES ('entregador123', 'moto123', '2025-01-01 00:00:00',
		'2025-01-07 23:59:59', '2025-01-07 23:59:59', 7, 30, '2024-12-31 12:00:00');

INSERT INTO public.rentals(
	delivery_man_id, motorcycle_id, start_date, 
	end_date, expected_end_date, plan, daily_value, created_date)
VALUES ('entregador123', 'moto123', '2025-02-01 00:00:00',
		'2025-02-15 23:59:59', '2025-02-15 23:59:59', 15, 28, '2025-01-30 12:00:00');