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





页面JS缺失问题解决办法：

## Running The Application

Before running the project, we need to run a npm task to bundle and minify the CSS and JavaScript files. In order to do that, we can open a command prompt, navigate to root directory of ***.Web.Mvc** project and run **npm run create-bundles** command. This command should be run when a new npm package is being added to the solution.