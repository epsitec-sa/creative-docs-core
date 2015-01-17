﻿CREATE VIEW SUBSCRIPTIONS
(
	ID,
	TYPE_ID,
	EM_ID,
	SUB_NUMBER,
	SUB_COUNT,
	"SUB_TYPE",
	REGIONAL_EDITION,
	HOUSEHOLD_ID,
	LEGALPERSON_ID,
	DISPLAY_NAME,
	DISPLAY_ADDRESS,
	DISPLAY_ZIPCODE,
	FLAG
)
AS
SELECT 
	CR_ID,
	CR_TYPE_ID,
	CR_EM_ID,
	U_LVG332,
	U_LVG432,
	U_LVG132,
	U_LVG532,
	U_LVGS22,
	U_LVG632,
	U_LVG932,
	U_LVGA32,
	U_LVGB32,
	U_LVABH
FROM MUD_LVGP22;


CREATE VIEW HOUSEHOLDS
(
	ID,
	TYPE_ID,
	EM_ID,
	LABEL,
	NAME,
	DISPLAY_NAME,
	DISPLAY_ADDRESS,
	ZIPCODE,
	VISIBILITY,
	PARISH_GROUP_PATH,
	ADDRESS_ID,
	COMMENT_ID
)
AS
SELECT 
	CR_ID,
	CR_TYPE_ID,
	CR_EM_ID,
	U_LVAG8,
	U_LVAH9,
	U_LVAGE,
	U_LVA5F,
	U_LVA6F,
	U_LVA8F,
	U_LVAAF,
	U_LVAT2,
	U_LVAA8
FROM MUD_LVAI2;


CREATE VIEW SUBSCRIPTIONREFUSALS
(
	ID,
	TYPE_ID,
	EM_ID,
	REFUSAL_TYPE,
	DISPLAY_NAME,
	DISPLAY_ADDRESS,
	ZIPCODE,
	HOUSEHOLD_ID,
	LEGALPERSON_ID
)
AS
SELECT 
	CR_ID,
	CR_TYPE_ID,
	CR_EM_ID,
	U_LVGF32,
	U_LVGG32,
	U_LVGH32,
	U_LVGI32,
	U_LVGD32,
	U_LVGE32
FROM MUD_LVGC32;