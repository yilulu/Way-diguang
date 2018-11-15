using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using DTcms.Common;

// 有關程式集的常規資訊通過以下特性集 
// 控制。更改這些特性值可修改
// 與程式集關聯的資訊。
[assembly: AssemblyTitle("DTcms Web DLL")]
[assembly: AssemblyDescription("DTcms 頁面類庫")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("DTcms")]
[assembly: AssemblyProduct("DTcms V" + DTKeys.ASSEMBLY_VERSION + " (.NET Framework 2.0)")]
[assembly: AssemblyCopyright("Copyright 2009 - " + DTKeys.ASSEMBLY_YEAR + " dtcms.net. All Rights Reserved.")]
[assembly: AssemblyTrademark("DTcms")]
[assembly: AssemblyCulture("")]

// 將 ComVisible 設置為 false 會使此程式集中的類型 
// 對 COM 組件不可見。如果需要從 COM 訪問此程式集中的類型， 
// 則將該類型上的 ComVisible 特性設置為 true。
[assembly: ComVisible(true)]

// 如果此項目向 COM 公開，則下列 GUID 用於型別程式庫的 ID
[assembly: Guid("17909288-0e5f-44f3-ac71-3af7304e56a3")]

// 程式集的版本資訊由下列四個值組成:
//
//      主版本
//      次版本 
//      內部版本號
//      修訂號
//
// 您可以指定所有這些值，也可以使用“修訂號”和“內部版本號”的預設值， 
// 方法是按如下所示使用“*”:
[assembly: AssemblyVersion(DTKeys.ASSEMBLY_VERSION)]
[assembly: AssemblyFileVersion(DTKeys.ASSEMBLY_VERSION)] 
