<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Skin-PredictionGame.ascx.cs"
    Inherits="TNGames.Controls.FrontEnd.Skin_PredictionGame" %>
<table width="100%">
    <tr>
        <td height="50" align="center" colspan="2">
            <table cellspacing="0" border="0" width="75%">
                <tbody>
                    <tr>
                        <td width="48">
                            <img height="48" width="48" src="/images/web/dudoangame.png">
                        </td>
                        <td align="center" class="namegame">
                            Thử tài dự đoán
                        </td>
                        <td>
                            <img height="48" width="48" src="/images/web/dudoangame.png">
                        </td>
                    </tr>
                </tbody>
            </table>
        </td>
    </tr>
</table>
<asp:Label ID="lblMsg" runat="server" ForeColor="#00FF00" />
<div id="prePlayedInfo" runat="server" class="PlayedInfo">
    <span style="color: #FFF;">Bạn đã tham gia trò chơi thử tài dự đoán. Bạn muốn cập nhật đáp án?</span><br /><br />
    <input id="ok" type="button" value="Đồng ý" class="buttonL" />
    <input type="button" value="Thoát" class="buttonL" onclick="location.href='/'" />
</div>
<div class="pregame" id="divContainer" runat="server">
    <table class="timerTable invisible" width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr <%= hfTimer.Value != "" ? "" : "class='invisible'" %>>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="center" valign="middle" class="scoresmall">
                            <%--  ĐIÊM--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="middle" class="score">
                            <%--   00--%>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="50%">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="center" valign="middle" class="scoresmall">
                            THỜI GIAN
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="middle" class="score">
                            <div id="timer">
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlQuestion" runat="server" CssClass="prediction">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <asp:Literal ID="litInfo" runat="server" Visible="false" />
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td height="60" align="center" class="questions">
                                        <asp:Literal ID="litQuestion" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:RadioButtonList ID="radList" runat="server">
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Bạn chưa chọn câu trả lời"
                                            ControlToValidate="radList" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnNext" runat="server" CssClass="buttonS" Text="Tiếp" OnClick="btnNext_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField ID="hfTimer" runat="server" />
            <asp:HiddenField ID="hfIndex" runat="server" />
            <asp:HiddenField ID="hfTotal" runat="server" />
            <asp:HiddenField ID="hfCache" runat="server" />
            <asp:HiddenField ID="hfID" runat="server" />
            <div class="timeout invisilbe">
                <h4>
                    <asp:Label ID="lblTimeout" runat="server" Text="Bạn đã hết thời gian qui định." />
                </h4>
                <asp:Button ID="btnSubmit" runat="server" CssClass="buttonL btnSubmit invisible last"
                    CausesValidation="false" Text="Kết thúc" OnClick="btnSubmit_Click" />
            </div>
            <asp:Panel ID="pnlSummary" runat="server" Visible="false">
                <h4>
                    <asp:Literal ID="litSummry" runat="server" />
                </h4>
                <h4>
                    <asp:Literal ID="litBonuss" runat="server" />
                </h4>
                <asp:Button ID="btnClose" CssClass="buttonS" runat="server" Text="Thoát" OnClick="btnClose_Click" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlStart" runat="server">
        <div class="startPreGame<%= hfTimer.Value != "" ? "" : " invisible" %>">
            <h4>
                Click nút "Bắt đầu" để thử tài dự đoán của bạn</h4>
            <input type="button" class="buttonL btnStart" value="Bắt đầu" />
        </div>
    </asp:Panel>
    <script type="text/javascript">
//<![CDATA[
        var interval = null;

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        function pageLoaded() {
            $('.last').click(function () {
                if (interval)
                    clearInterval(interval);
            })

            $('#radList label').each(function (i, e) {
                $(e).addClass('uncheck');
            })

            $('#radList input[type=radio]').click(function () {
                $('#radList .check').removeClass('check')
                                    .addClass('uncheck');
                if (this.checked) {
                    $(this).next().addClass('check');
                }
            })

            // checked check item
            $('#radList :radio[checked]').next().removeClass('uncheck').addClass('check');
        }

        $('.btnStart').click(function () {
            if ($('input[id$=hfTimer]') != '') {
                countDown();
                interval = setInterval(countDown, 1000);
                $('.prediction').slideDown();
                $('.startPreGame').hide();
                $('.timerTable').removeClass('invisible');
            }
        })

        function countDown() {
            timer = $('input[id$=hfTimer]');
            count = parseInt(timer.val());

            if (count >= 0) {
                hours = parseInt(count / 3600);
                hours = hours > 9 ? hours + '' : '0' + hours;

                mins = parseInt((count - (hours * 3600)) / 60);
                mins = mins > 9 ? mins + '' : '0' + mins;

                secs = parseInt(count - (hours * 3600) - (mins * 60));
                secs = secs > 9 ? secs + '' : '0' + secs;

                strTimer = hours + ":" + mins + ":" + secs;
                $('#timer').html(strTimer);
            }

            if (count == 0) {
                $('.timeout').slideDown();
                $('.prediction').slideUp();
                $('.btnSubmit').show();
            }

            if (count > 0)
                count--;
            timer.val(count);
        }

   
//]]>
    </script>
</div>

 <script type="text/javascript">
//<![CDATA[

     $('#ok').click(function () {
         $('.PlayedInfo').hide();
         $('.btnStart').click();
         $('.pregame').slideDown();
     }) 
       
//]]>
</script>