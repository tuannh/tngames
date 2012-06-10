<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Skin-BettingGame.ascx.cs"
    Inherits="TNGames.Controls.FrontEnd.Skin_BettingGame" %>
<table cellspacing="0" cellpadding="0" border="0" width="100%">
    <tbody>
        <tr>
            <td class="bgcacuocAleft">
                &nbsp;
            </td>
            <td align="left" valign="top" style="padding-top: 22px;" class="bgcacuocAcenter">
                <div class="bettingContainer">
                    <div class="namegame invisible" style="margin-bottom: 5px;">
                        Thử tài phân tích trận đấu
                    </div>
                    <asp:Label ID="lblMsg" runat="server" />
                    <asp:Repeater ID="rptList" runat="server" OnItemDataBound="rptList_ItemDataBound"
                        ClientIDMode="Predictable">
                        <ItemTemplate>
                            <a href="#b<%# Eval("Id") %>" class="bname bgbutton" id="b<%# Eval("Id") %>a">
                                <asp:Literal ID="litBettingInfo" runat="server" />
                            </a>
                            <div class="invisible">
                                <div id="b<%# Eval("Id") %>">
                                    <div class="id invisible">
                                        <%# Eval("Id") %></div>
                                    <div class="rateId invisible">
                                        <asp:Literal ID="litRateId" runat="server" /></div>
                                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                        <tr>
                                            <td class="bgcacuocAleft">
                                                &nbsp;
                                            </td>
                                            <td align="center" class="bgcacuocAcenter">
                                                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                    <tr>
                                                        <td width="163">
                                                            <div class="bettingTeam">
                                                                <div class="scoresmall">
                                                                    <%# Eval("HomeTeam")%>
                                                                </div>
                                                                <div class="bgscore">
                                                                    <asp:Literal ID="litHomeRate" runat="server" />
                                                                </div>
                                                                <label class="bettingUncheck teama">
                                                                    <asp:RadioButton ID="radTeamA" runat="server" />
                                                                    <span class="invisible">
                                                                        <%# "#b"+ Eval("Id") %></span>
                                                                </label>
                                                            </div>
                                                        </td>
                                                        <td valign="top">
                                                            <div class="time">
                                                                <div class="scoresmall">
                                                                    Thời gian</div>
                                                                <div class="score">
                                                                    <asp:Literal ID="litPlayDate" runat="server" />
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td width="163">
                                                            <div class="bettingTeam">
                                                                <div class="scoresmall">
                                                                    <%# Eval("VisitingTeam")%>
                                                                </div>
                                                                <div class="bgscore">
                                                                    <asp:Literal ID="litVisitingRate" runat="server" />
                                                                </div>
                                                                <label class="bettingUncheck teamb">
                                                                    <asp:RadioButton ID="radTeamB" runat="server" />
                                                                    <span class="invisible">
                                                                        <%# "#b"+ Eval("Id") %></span>
                                                                </label>
                                                            </div>
                                                        </td>
                                                        <td width="150" valign="top">
                                                            <div class="bettingPoint">
                                                                <div class="scoresmall">
                                                                    SỐ ĐIỂM
                                                                </div>
                                                                <div class="scorePoint">
                                                                    <asp:TextBox ID="txtPoint" runat="server" CssClass="txtPoint" MaxLength="5" ValidationGroup='<%# "Group" + Eval("Id") %>' />
                                                                    <%--  <asp:RequiredFieldValidator ID="rfv" runat="server" ErrorMessage="*" ControlToValidate="txtPoint"
                                                ForeColor="Red" ValidationGroup='<%# "Group" + Eval("Id") %>' />--%>
                                                                    <act:FilteredTextBoxExtender ID="ftx" runat="server" FilterType="Numbers" TargetControlID="txtPoint" />
                                                                </div>
                                                                <asp:Button ID="btnSubmit" runat="server" Text="ĐỒNG Ý" CssClass="buttonL btnSubmit"
                                                                    ValidationGroup='<%# "Group" + Eval("Id") %>' />
                                                                <span class="invisible">#b<%# Eval("Id") %></span>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="bgcacuocAright">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <%# (Container.ItemIndex + 1) % 2 == 0 ? "<div class='clear'></div>" : "" %>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </td>
            <td valign="middle" align="center" class="bgcacuocAright">
                &nbsp;
            </td>
        </tr>
    </tbody>
</table>
<script type="text/javascript">
//<![CDATA[
    $('.bname').each(function () {
        //$(this).next().slideToggle();
        $(this).fancybox({
        //  'hideOnContentClick': false
            onStart: hideFlash,
            onClosed: showFlash
        });
    })

 function hideFlash() {
        $('embed, object, iframe').css('visibility', 'hidden');
    }
    function showFlash() {
        $('embed, object, iframe').css({ 'visibility': 'visible' });
    }


    $('.bettingTeam label input').click(function () {
        var rId = $(this).next().html();
        $(rId + ' .bettingTeam label').removeClass('bettingCheck').addClass('bettingUncheck');
        $(this).parent().removeClass('bettingUncheck').addClass('bettingCheck');
    })

    var aid;
    var pid;
    $('.btnSubmit').click(function () {
        pid = $(this).next().html();
        aid = pid + 'a';
        if (validSubmit(pid)) {
            var sid = $(pid + " .id").html();
            var steam = $(pid + ' .teama input')[0].checked ? 'a' : 'b';
            var spoint = $(pid + " .txtPoint").val();
            var rateId = $(pid + " .rateId").html();

            $.post("/tro-choi/dat-cuoc", { id: sid, team: steam, point: spoint, rateId: rateId }, submitCallBack);
        }

        return false;
    })

    function submitCallBack(data) {
        arr = data.split(',');
        if (arr[0] == '1') {

            // update current point
            $.get('check.ashx?key=point', function (data) {
                var arr = data.split(";");
                if (arr.length == 2) {
                    $("#totalPoint").html(arr[0]);
                    $("#cpoint").html(arr[1]);
                }
            });

            alert(arr[1]);
            $.fancybox.close();
            $(aid).unbind('click.fb');
            $(aid).addClass('betted');
            // location.href = location.href;
        }
        else {
            alert(arr[1]);
        }
    }

    function validSubmit(pid) {
        var teama = $(pid + ' .teama input')[0].checked;
        var teamb = $(pid + ' .teamb input')[0].checked;
        var p = $(pid + " .txtPoint").val();

        if (p == '' || p == '0') {
            alert('Bạn bạn chưa nhập số điểm');
            return false;
        }

        var cp = parseInt($('#cpoint').html());
        if (cp < parseInt(p)) {
            alert('Số điểm phải nhỏ hơn hoặc bằng số điểm hiện có.\nSố điểm hiện tại của bạn ' + cp);
            return false;
        }

        if (!teama && !teamb) {
            alert('Bạn phải chọn 1 đội trước khi nhấn nút "ĐỒNG Ý"');
            return false;
        }

        return true;
    }

    function removeBetting(pid) {
        $(pid).prev().unbind('click');
        $(pid).remove();
    }

//]]>
</script>
