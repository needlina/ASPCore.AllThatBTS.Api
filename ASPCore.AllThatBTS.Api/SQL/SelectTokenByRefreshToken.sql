﻿SELECT 
	USER_NO
	,ACCESS_TOKEN
	,REFRESH_TOKEN
	,SCOPE
	,ACCESS_EXPIRE_DT
	,REFRESH_EXPIRE_DT
	,CREATE_DT
	,UPDATE_DT
FROM TB_AUTH
WHERE REFRESH_TOKEN = @REFRESH_TOKEN