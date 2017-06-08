#region Copyright
// 
// DotNetNuke� - http://www.dotnetnuke.com
// Copyright (c) 2002-2014
// by DotNetNuke Corporation
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion
#region Usings

using System;

using DotNetNuke.Entities.Modules;
using DotNetNuke.Instrumentation;
using Dnn.Modules.Dashboard.Components.Database;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Skins.Controls;

#endregion

namespace Dnn.Modules.Dashboard.Controls
{
    public partial class Database : PortalModuleBase
    {
    	private static readonly ILog Logger = LoggerSource.Instance.GetLogger(typeof (Database));
        protected string NoBackups
        {
            get
            {
                return Localization.GetString("NoBackups", LocalResourceFile);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DbInfo database = DatabaseController.GetDbInfo();
            ctlDbInfo.DataSource = database;
            ctlDbInfo.DataBind();
            //Localization.LocalizeGridView(ref grdBackups, LocalResourceFile);
            try
            {
                grdBackups.DataSource = database.Backups;
                grdBackups.DataBind();
            }
            catch (Exception exc)
            {
                Logger.Error(exc);

                grdBackups.Visible = false;
                DotNetNuke.UI.Skins.Skin.AddModuleMessage(Parent, Localization.GetString("Backup.Error.Text", LocalResourceFile), ModuleMessage.ModuleMessageType.YellowWarning);
            }
            //Localization.LocalizeGridView(ref grdFiles, LocalResourceFile);
            grdFiles.DataSource = database.Files;
            grdFiles.DataBind();
        }
    }
}