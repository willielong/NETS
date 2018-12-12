<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true"
    CodeBehind="SignatureList.aspx.cs" Inherits="vxTalent.WebSite.Pages.ESignature.SignatureList" %>

<%@ Register Assembly="vxTalent.WebControls" Namespace="vxTalent.WebControls" TagPrefix="vx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=ResolveUrl("~/Content/weather/css/weather-icons.css")%>" rel="stylesheet"
        type="text/css" />
    <link href="<%=ResolveUrl("~/Content/vx_fonts/vx_fonts.css")%>" rel="stylesheet"
        type="text/css" />
    <script src="<%=ResolveUrl("~/Scripts/jsviews.min.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/Scripts/Number.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/Scripts/vxQuickMenu.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/Scripts/vxTempTable.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/Scripts/vxMovePanel.js")%>" type="text/javascript"></script>
    <style type="text/css">
        .form-group-query input
        {
            margin-left: 20px;
        }
        .knxLabel, .knxLabelText
        {
            overflow: hidden;
            text-overflow: ellipsis;
        }
        .MonthReportRecord
        {
            height: 24px;
            width: 88px;
            margin: 1px 0 0 1px !important;
        }
        .MonthReportTextbox
        {
            height: 24px;
            width: 88px;
            margin: 1px 0 0 1px !important;
        }
        .fontbtn-notice
        {
            width: 14px;
            height: 14px;
            font-family: FontAwesome;
            font-size: 14px;
            font-weight: 400;
            line-height: 1;
            color: #0099e5;
        }
        #ctl00_main_LstMonthReport_Freeze tr, #ctl00_main_LstMonthReport_Freeze tr td
        {
            height: 30px;
        }
        #ctl00_main_LstMonthReport_Freeze tr:first-child, #ctl00_main_LstMonthReport_Freeze tr:first-child td
        {
            height: 35px;
        }
        #ctl00_main_LstMonthReport_FreezeLast tr, #ctl00_main_LstMonthReport_FreezeLast tr td
        {
            height: 30px;
        }
        #ctl00_main_LstMonthReport_FreezeLast tr:first-child, #ctl00_main_LstMonthReport_FreezeLast tr:first-child td
        {
            height: 36px;
        }
        #ctl00_main_LstMonthReport tr, #ctl00_main_LstMonthReport tr td
        {
            height: 30px;
        }
        #ctl00_main_LstMonthReport tr:first-child, #ctl00_main_LstMonthReport tr:first-child td
        {
            height: 35px;
        }
        #ctl00_main_LstMonthReport tr:first-child td div
        {
            white-space: nowrap;
            word-break: keep-all;
            overflow: hidden;
            text-overflow: ellipsis;
        }
        .a_activie
        { cursor:pointer
            }
    </style>
    <script type="text/javascript">
    var point=5;
        var modelData;
        $(function () {
            loadPage(1);
        });
        Number.prototype.ToPage = function (RecPerPage) {
            var adjust = 0;
            if (this % RecPerPage > 0) {
                adjust = 1;
            }
            return parseInt(this / RecPerPage + "", 10) + adjust;
        }
        ///初始化数据
        function InitData(page) {
            var applyName = $("#<%=__ExaminationApply.ClientID %>").val();
            var startTime = $("#<%=__StartDate.ClientID %>").val();
            var endTime = $("#<%=EndDate.ClientID %>").val();
            var flowType = $("#<%=FlowType.ClientID %>").val();
            var flowName = $("#<%=ddlWorkFlowType.ClientID %>").val();
            $.ajax({
                type: "post",
                url: "SignatureList.aspx/GetList",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async:false,
                data: JSON.stringify({ applyName: applyName, startTime: startTime, endTime: endTime, flowType: flowType, flowName: flowName, page: page }),
                success: function (result) {
                    var jsonObj = JSON.parse(result.d);
                    if (jsonObj != null) {
                        modelData = jsonObj;//console.log(jsonObj);                        

                        //PageJump(1);
                        //loadPage(1);
                    }
                },
                error: function (result) {
                }
            });
        }
        var InitBind = function () {
            $.templates({
                Tmpl: {
                    markup: "#contentTmpl",
                    //                    helpers: {
                    //                        GetTitleName: GetTitleName,
                    //                        GetAutoFieldValue: GetAutoFieldValue,
                    //                        GetTitleToolTip: GetTitleToolTip
                    //                    }
                }
            });
            $.link.Tmpl("#renderContent", modelData);
        }
        function PageJump(CurrentPage) {
            if (modelData.B.length < 1)//没数据
            {
                $("#trhasdata").css("display", "none");
                $("#trnodata").css("display", "");
            }
            else {
                $("#trhasdata").css("display", "");
                $("#trnodata").css("display", "none");
            }
        }
        ///加载页码
        function loadPage(page) {
            InitData(page);
            modelData.Pages = [];
            var Pges = [];
            if (page == 1) {
                if (modelData.pageCount > point) {

                    for (var i = 1; i <= point; i++) {
                        if (i == 1) {
                            Pges.push({ page: 1, isAct: true });
                        } else {
                            Pges.push({ page: i, isAct: false });
                        }
                    }
                } else {

                    for (var i = 1; i <= modelData.pageCount; i++) {
                        if (i == 1) {
                            Pges.push({ page: 1, isAct: true });
                        } else {
                            Pges.push({ page: i, isAct: false });
                        }
                    }
                }
            } else {
                if (page == modelData.pageCount) {
                    var s = (parseInt(page)) % point;
                    if (s != 0) {
                        for (var i = s; i > 0; i--) {
                            if (i > 1) {
                                if (i == s) Pges.push({ page: parseInt(page) - i, isAct: true });
                                else
                                    Pges.push({ page: parseInt(page) - i, isAct: false });
                            } else {
                                Pges.push({ page: parseInt(page), isAct: true });
                            }
                        }
                    }
                    else {
                         for (var i = point; i > 0; i--) {
                            if (i > 1) {
                                if (i == point) Pges.push({ page: parseInt(page) - i, isAct: true });
                                else
                                    Pges.push({ page: parseInt(page) - i, isAct: false });
                            } else {
                                Pges.push({ page: parseInt(page), isAct: true });
                            }
                        }
                    }
                }

            } $.observable(modelData.Pages).insert(Pges);
             $("#hdFristPage").val(page);
            InitBind();
        }

         function clickPage(page) {
         var oldPages=modelData.Pages;
            var hdPage = $("#hdFristPage").val();
            InitData(page);
            var Pges = [];
            if (parseInt(hdPage) > page) {
                if (oldPages[0].page == 1) {
                    Pges =oldPages;
                      for (var i = 0; i < Pges.length; i++) {
                        if (Pges[i].page == page) Pges[i].isAct = true;
                        else Pges[i].isAct = false
                    }  
                }
                else {
                    for (var i =0; i < oldPages.length; i++) {
                        if (oldPages.page - 1 == page) {
                            Pges.push({ page: parseInt(oldPages[i].page) - 1, isAct: true });
                        } else {
                            Pges.push({ page: parseInt(oldPages[i].page) - 1, isAct: false });
                        }
                    }
                }
            } else {
                if (max(oldPages) == modelData.pageCount) {
                    Pges = oldPages;
                          for (var i = 0; i < Pges.length; i++) {
                         if (Pges[i].page == page) Pges[i].isAct = true;
                         else Pges[i].isAct = false;
                    }  
                }
                else {
                    for (var i = 0; i < oldPages.length; i++) {
                        if (oldPages[i].page + 1 == page) {
                            Pges.push({ page: parseInt(oldPages[i].page) + 1, isAct: true });
                        } else {
                            Pges.push({ page: parseInt(oldPages[i].page) +1, isAct: false });
                        }
                    }
                }
            }
            modelData.Pages = [];
             $("#hdFristPage").val(page);
            $.observable(modelData.Pages).insert(Pges);
            InitBind();
        }
        function max(pages) {
            var num = pages[0].page;
            for (var i = 0; i < pages.length; i++) {
                if (num < pages[i].page) {
                    num = pages[i].page;
                }
            }
            return num;
        }
        function bigPage(page) {
            $("#ul_page").after("");
            $("#ul_page").after("<li class='active'><a href=''>" + (parseInt(page) - 2) + "</a></li><li><a href=''>" + (parseInt(page) - 1) + "</a></li><li><a href=''>" + page + "</a></li>");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <input id="hdValidateMsg" name="hdValidateMsg" type="hidden" value="" />
    <input id="hdFristPage" name="hdFristPage" type="hidden" value="" />
    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="0" runat="server">
    </asp:ScriptManager>
    <div class="layout-panel layout-bottom">
        <div class="layout-panel-content">
            <fieldset class="form-column-3">
                <vx:vxnormaltext id="__ExaminationApply" runat="server" controlfill="False" controlindent="True"></vx:vxnormaltext>
                <vx:vxdatebox id="__StartDate" runat="server"> </vx:vxdatebox>
                <vx:vxdatebox id="EndDate" runat="server"></vx:vxdatebox>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                    <ContentTemplate>
                        <vx:vxdropdownlist id="FlowType" autopostback="true" controlfill="False" dataobjectname="TableType"
                            runat="server" onselectedindexchanged="TableType_Changed"></vx:vxdropdownlist>
                        <vx:vxdropdownlist id="ddlWorkFlowType" controlfill="False" runat="server"></vx:vxdropdownlist>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="FlowType" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="form-group form-group-query">
                    <input type="button" name="kkmain_btnQuery" value="<%=Resource("_Search")%>" id="kkmain_btnQuery"
                        class="btn btn-default knxFormButton submit search" />
                </div>
            </fieldset>
            <div id="renderContent" class="layout-table">
            </div>
                        
        </div>
    </div>
    <script id="contentTmpl" type="text/x-jsrender">
                <div class="tab-pane fade in active" id="TodoList">
                    <div class="layout-table">
                    <input type="hidden" id="Hidden1" name="hdnctl00_main_TodoListTable"
                        value=""/>
                        <div class="layout-table-header">
                        <span class="span-9"></span><span title="" class="span-6">{{:title}}</span><span class="span-9">
                        <ul class="pagination" id="ul_page" style="display:none">
                            <li id="li_fristPage"><a class="a_activie" onclick="loadPage(1);"><i class="vx-icon-first-page"></i></a></li>
                            {^{for Pages}}
                              {^{if isAct==true}}
                               <li class='active'><a class="a_activie" onclick="clickPage({{:page}})">{^{:page}}</a></li>
                               {^{else}}
                               <li><a class="a_activie" onclick="clickPage({{:page}})">{^{:page}}</a></li>
                              {{/if}}
                            {{/for}}                           
                            <li id="li_NextPage"><a class="a_activie" onclick="loadPage({{:pageCount}});"><i class="vx-icon-last-page"></i></a></li>
                            <li><span class="pagination-jump">
                                <input type="text" class="pagination-editor" data-total-page="6" name="turnpage_page_TodoListTable"
                                    id="Text1" onchange="this.value=this.value.replace(/[^0-9-]+/,&quot;&quot;); "
                                    onkeyup="value=value.replace(/[^0-9_]/g,&quot;&quot;)" onkeydown="kd(event)"
                                    onpaste="return false" value="{{:CurrentPage}}" title="回车键翻页">/{{:pageCount}}页</span></li>                            
                        </ul>
                        </span>
                      </div>
                      <div id="tbl_ctl00_main_TodoListTable" class="layout-table-content  tbl  tbl-simple  tbl-striped tbl-hover  responsive-height  tbl-inited"
                        style="height: 561px;">
                        <div class="tbl-header">
                            <table rdonly="0" rc="105" tablename="ctl00_main_TodoListTable" flc="0" freeze="1"
                                style="width: 100%; min-width: 795px;">
                                <tbody>
                                    <tr class="TTHeaderNotOverFlow">
                                    <td fld="RowNumber" ci="0">
                                            <a  class="TTTextALink" 
                                                title='<%=Resource("RowNum") %>'><span><%=Resource("RowNum") %></span><div class="sort-arrow">
                                                </div>
                                            </a>
                                        </td>
                                         <td fld="typeName" ci="0">
                                           <a  class=TTTextALink" 
                                                title='<%=Resource("FlowType") %>'><span><%=Resource("FlowType") %></span><div class="sort-arrow">
                                                    <i class="arrow-up"></i><i class="arrow-down"></i>
                                                </div>
                                            </a>
                                           
                                        </td>
                                        <td fld="flowName" ci="0" >
                                            <a href= class="TTTextALink" 
                                                title='<%=Resource("FlowName") %>'><span><%=Resource("FlowName") %></span><div class="sort-arrow">
                                                    <i class="arrow-up"></i><i class="arrow-down"></i>
                                                </div>
                                            </a>
                                        </td>
                                         <td fld="flowName" ci="0" >
                                            <a  class="TTTextALink"
                                                title='<%=Resource("E_FlowTitle") %>'><span><%=Resource("E_FlowTitle") %></span><div class="sort-arrow">
                                                    <i class="arrow-up"></i><i class="arrow-down"></i>
                                                </div>
                                            </a>
                                        </td>
                                        <td fld="flowName" ci="0" >
                                            <a  class="TTTextALink" 
                                                title='<%=Resource("E_ApplicationName") %>'><span><%=Resource("E_ApplicationName") %></span><div class="sort-arrow">
                                                    <i class="arrow-up"></i><i class="arrow-down"></i>
                                                </div>
                                            </a>
                                        </td>
                                         <td fld="flowName" ci="0" >
                                            <a  class="TTTextALink" 
                                                title='<%=Resource("E_ApplicationDate") %>'><span><%=Resource("E_ApplicationDate") %></span><div class="sort-arrow">
                                                    <i class="arrow-up"></i><i class="arrow-down"></i>
                                                </div>
                                            </a>
                                        </td>
                                         <td fld="flowName" ci="0" >
                                            <a  class="TTTextALink"
                                                title='<%=Resource("Operation") %>'><span><%=Resource("Operation") %></span><div class="sort-arrow">
                                                    
                                                </div>
                                            </a>
                                        </td>                                        
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="tbl-content ps ps--theme_default ps--active-y" data-ps-id="d35fc6cd-97c4-b948-7461-073b0f762ece"
                            style="height: 527px;">
                            <table id="ctl00_main_TodoListTable" class="TempTableOuter" cellspacing="1" cellpadding="2"
                                rdonly="0" rc="105" tablename="ctl00_main_TodoListTable" flc="0" style="width: 100%;
                                min-width: 795px;">
                                <tbody>

                                 {^{for B}}
                                    <tr class="rowEven" key="11" del="0" >                                       
                                        <td style="text-align: center;">
                                            <div style="text-align: center; width: 50px; white-space: nowrap;" class="TTableBodyText">
                                                {{:RowNumber}}</div>
                                        </td>
                                        <td style="text-align: Left;">
                                            <div style="text-align: Left; width: 80px; white-space: nowrap;" class="TTableBodyText"
                                                title="{{:typeName}}">
                                                {{:typeName}}</div>
                                        </td>
                                        <td style="text-align: Left;">
                                            <div style="text-align: Left; width: 85px; white-space: nowrap;" class="TTableBodyText"
                                                title="{{:flowName}}">
                                                {{:flowName}}
                                            </div>
                                        </td>
                                        <td style="text-align: Left;">
                                            <div style="text-align: Left; width: 100%; white-space: nowrap;" class="TTableBodyText"
                                                title="{{:Title}}">
                                                {{:Title}}
                                            </div>
                                        </td>
                                        <td style="text-align: Left;">
                                            <div style="text-align: Left; width: 100%; white-space: nowrap;" class="TTableBodyText"
                                                title="{{:SenderName}}">
                                                {{:SenderName}}
                                            </div>
                                        </td>
                                        <td style="text-align: Left;">
                                            <div style="text-align: Left; width: 70px; white-space: nowrap;" class="TTableBodyText"
                                                title="{{:SenderTime}}">
                                                {{:SenderTime}}
                                            </div>
                                        </td>
                                         <td style="text-align: Left;">
                                            <div style="text-align: Left; width: 70px; white-space: nowrap;" class="TTableBodyText"
                                                title="{{:SenderTime}}">
                                                {{:SenderTime}}
                                            </div>
                                        </td>
                                        <td style="text-align: center;">
                                            <div style="text-align: center; white-space: nowrap;width:80px;" class="TTText">
                                                <div style="text-align: center">                      
			                                        <a id="delrow" class="listbtn-del" title="<%=Resource("ViewFrom") %>">&nbsp;&nbsp;&nbsp;&nbsp;</a>    
                                                    <a id="delrow" class="listbtn-del" title="<%=Resource("DownloadPdf") %>">&nbsp;&nbsp;&nbsp;&nbsp;</a>                      
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    {^{/for}}
                                </tbody>
                            </table>                            
                        </div>
                    </div>
                    </div>
                </div>
            </script>
</asp:Content>
