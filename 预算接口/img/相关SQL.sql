--预算池 研发中心
SELECT bp.BudgetPoolID
,bp.DepartID
,va.NamePath
,bp.TrackType
,ft.TrackName
,bp.Month
,bp.Year
,bp.ProjectName
,bp.MonthValue
,bp.YearValue
 FROM dbo.BudgetPool bp
LEFT JOIN dbo.v_DepartMentAchievement va ON va.DepartID = bp.DepartID
LEFT JOIN dbo.BudgetFieldTrackType ft ON ft.TrackCode=bp.TrackType
WHERE bp.DepartID IN (SELECT * FROM dbo.f_GetChildAchDepartidInUse('DP01023'))
AND TrackType='QB_08'
AND	Year=2019
AND	Month=4

--跟踪方式
SELECT * FROM dbo.BudgetFieldTrackType

--OA流水
SELECT * FROM dbo.OABudgetDetails ORDER BY RecID DESC

--Crm项目使用
SELECT * FROM dbo.BudgetIncreaseDeduction  ORDER BY RecID DESC

--管理员调整
SELECT * FROM dbo.BudgetManagerIncreaseDeduction ORDER BY RecID DESC

--上月残值
SELECT * FROM BudgetSurplusDetails ORDER BY RecID DESC