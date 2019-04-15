# ABP代码生成

- 安装AspNetZeroRadToolVisualStudioExtension1.7.0（VS插件）

- 修改源码中的 aspnet-core\AspNetZeroRadTool\config.json

  ```properties
  {
    "CompanyName": "MyCompanyName",  --公司名
    "ProjectName": "AbpZeroTemplate",--项目名
    "ProjectType": "Mvc",			   --"Mvc"、“Angular"或“AngularMerged（可能不能用）" 
    "ProjectVersion":"ProjectVersion",
    "ApplicationAreaName": "AppAreaName",
    "AngularSrcPath": "\\..\\..\\angular\\src\\",
    "CoreSrcPath": "\\..\\src\\",
    "LicenseCode": "LicenseCodePlaceHolderToReplace"
  }
  ```

