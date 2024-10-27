using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace InpolTempStay
{
    internal class BotTempStayHTTP : BotTempStay
    {
        public BotTempStayHTTP(Action<string> log) : base(log)
        {
        }

        //public override async Task Start(Config config, FormInfo formInfo, CancellationToken ct)
        //{
        //    await Task.Factory.StartNew(async () =>
        //    {
        //        log($"Started for {formInfo.ImieCudzoziemca} {formInfo.NazwiskoCudzoziemca}");

        //        while(true)
        //        {
        //            try
        //            {
        //                var formData = await SendGet(config, formInfo);
        //                await SendForm(formData);
        //            }
        //            catch(IncapsulaException)
        //            {
        //                log("Incapsula detected, solving");
        //                continue;
        //            }
        //            catch(Exception e)
        //            {
        //                log("Erorr: " + e.ToString());
        //                continue;
        //            }
        //        }

        //        log($"Filled for {formInfo.ImieCudzoziemca} {formInfo.NazwiskoCudzoziemca}");

        //        if(ct.IsCancellationRequested)
        //            return;
        //    });
        //}

        //class IncapsulaException : Exception
        //{

        //}
        //class FormData
        //{
        //    public string Nonce { get; set; }
        //    public string RecaptchaSolution { get; set; }
        //    public FormInfo FormInfo { get; set; }
        //}

        //async Task<FormData> SendGet(Config config, FormInfo formInfo)
        //{
        //    var URL = "https://pobyt-czasowy-zapis-na-zlozenie-wniosku.mazowieckie.pl/";
        //    var client = new HttpClient();
        //    var req = new HttpRequestMessage(HttpMethod.Get, URL);
        //    req.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");
        //    var res = await client.SendAsync(req);
        //    var resStr = await res.Content.ReadAsStringAsync();
        //    if(resStr.Contains("Request unsuccessful. Incapsula incident ID"))
        //        throw new IncapsulaException();
        //    var regexRecaptchaSitekey = new Regex(@"data-sitekey=""(6.*?)""");
        //    var sitekey = regexRecaptchaSitekey.Match(resStr).Groups[1].Value.ToString();

        //    var regexNonce = new Regex(@"""ajaxNonce"":""(.*?)""");
        //    var nonce = regexNonce.Match(resStr).Groups[1].Value.ToString();


        //    /// solve captcha
        //    var solver = new CaptchaSolver(config.DBCLogin, config.DBCPass);
        //    var captchaSolution = solver.SolveCaptcha(URL, sitekey);

        //    return new FormData()
        //    {
        //        FormInfo = formInfo,
        //        Nonce = nonce,
        //        RecaptchaSolution = captchaSolution
        //    };
        //}

        //        public async Task SendForm(FormData data)
        //        {
        //            var URL = "https://pobyt-czasowy-zapis-na-zlozenie-wniosku.mazowieckie.pl/wp-admin/admin-ajax.php";
        //            var client = new HttpClient();
        //            var req = new HttpRequestMessage(HttpMethod.Post, URL);
        //            req.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");
        //            req.Headers.Add("Referer", "https://pobyt-czasowy-zapis-na-zlozenie-wniosku.mazowieckie.pl/");
        //            req.Headers.Add("Origin", "https://pobyt-czasowy-zapis-na-zlozenie-wniosku.mazowieckie.pl");
        //            req.Headers.Add("X-Requested-With", "XMLHttpRequest");

        //            var fromData = $@"{{
        //   ""id"":""6"",
        //   ""fields"":{{
        //      ""66"":{{
        //         ""value"":""{data.FormInfo.ImieCudzoziemca}"",
        //         ""id"":66
        //      }},
        //      ""67"":{{
        //         ""value"":""{data.FormInfo.NazwiskoCudzoziemca}"",
        //         ""id"":67
        //      }},
        //      ""69"":{{
        //         ""value"":""{data.FormInfo.EmailPowiadomienie}"",
        //         ""id"":69
        //      }},
        //      ""70"":{{
        //         ""value"":"""",
        //         ""id"":70
        //      }},
        //      ""71"":{{
        //         ""value"":""{data.RecaptchaSolution}"",
        //         ""id"":71
        //      }},
        //      ""72"":{{
        //         ""value"":1,
        //         ""id"":72
        //      }},
        //      ""74"":{{
        //         ""value"":""<p><b><a+href=\""https://pobyt-czasowy-zapis-na-zlozenie-wniosku.mazowieckie.pl/wp-content/uploads/2024/03/REGULAMIN-FORMULARZ-ZAPISOW-POBYTY-CZASOWE.pdf\"">REGULAMIN</a></b><br><br></p>"",
        //         ""id"":74
        //      }},
        //      ""75"":{{
        //         ""value"":""{data.FormInfo.PobytNaPodstawie}"",
        //         ""id"":75
        //      }},
        //      ""78"":{{
        //         ""value"":""<p><br></p>"",
        //         ""id"":78
        //      }},
        //      ""79"":{{
        //         ""value"":"""",
        //         ""id"":79
        //      }},
        //      ""80"":{{
        //         ""value"":""{data.FormInfo.KodWnioskuInpol}"",
        //         ""id"":80
        //      }},
        //      ""81"":{{
        //         ""value"":""{data.FormInfo.KodWnioskuMOS}"",
        //         ""id"":81
        //      }},
        //      ""82"":{{
        //         ""value"":""{data.FormInfo.DataUrodzenia}"",
        //         ""id"":82
        //      }},
        //      ""83"":{{
        //         ""value"":""{data.FormInfo.NumerPaszportu}"",
        //         ""id"":83
        //      }},
        //      ""84"":{{
        //         ""value"":""{data.FormInfo.Obywatelstwo}"",
        //         ""id"":84
        //      }},
        //      ""85"":{{
        //         ""value"":""{data.FormInfo.DataWaznosciDokumentu}"",
        //         ""id"":85
        //      }},
        //      ""86"":{{
        //         ""value"":""{data.FormInfo.DanePelnomocnika}"",
        //         ""id"":86
        //      }},
        //      ""92"":{{
        //         ""value"":""{data.FormInfo.NumerTelefonuRP}"",
        //         ""id"":92
        //      }}
        //   }},
        //   ""settings"":{{
        //      ""objectType"":""Form+Setting"",
        //      ""editActive"":true,
        //      ""title"":""Pobyt+czasowy"",
        //      ""created_at"":""{DateTime.Now.ToString("yyyy-MM-dd+HH:mm:ss")}"",
        //      ""default_label_pos"":""above"",
        //      ""show_title"":""0"",
        //      ""clear_complete"":""1"",
        //      ""hide_complete"":""1"",
        //      ""logged_in"":""0"",
        //      ""wrapper_class"":"""",
        //      ""element_class"":"""",
        //      ""key"":"""",
        //      ""add_submit"":""1"",
        //      ""currency"":"""",
        //      ""unique_field_error"":""Formularz+z+tą+wartością+został+już+przesłany."",
        //      ""not_logged_in_msg"":"""",
        //      ""sub_limit_msg"":""<p><!--[if+gte+mso+9]><xml>\n+<o:OfficeDocumentSettings>\n++<o:AllowPNG/>\n+</o:OfficeDocumentSettings>\n</xml><![endif]--><!--[if+gte+mso+9]><xml>\n+<w:WordDocument>\n++<w:View>Normal</w:View>\n++<w:Zoom>0</w:Zoom>\n++<w:TrackMoves/>\n++<w:TrackFormatting/>\n++<w:HyphenationZone>21</w:HyphenationZone>\n++<w:PunctuationKerning/>\n++<w:ValidateAgainstSchemas/>\n++<w:SaveIfXMLInvalid>false</w:SaveIfXMLInvalid>\n++<w:IgnoreMixedContent>false</w:IgnoreMixedContent>\n++<w:AlwaysShowPlaceholderText>false</w:AlwaysShowPlaceholderText>\n++<w:DoNotPromoteQF/>\n++<w:LidThemeOther>PL</w:LidThemeOther>\n++<w:LidThemeAsian>X-NONE</w:LidThemeAsian>\n++<w:LidThemeComplexScript>X-NONE</w:LidThemeComplexScript>\n++<w:Compatibility>\n+++<w:BreakWrappedTables/>\n+++<w:SnapToGridInCell/>\n+++<w:WrapTextWithPunct/>\n+++<w:UseAsianBreakRules/>\n+++<w:DontGrowAutofit/>\n+++<w:SplitPgBreakAndParaMark/>\n+++<w:EnableOpenTypeKerning/>\n+++<w:DontFlipMirrorIndents/>\n+++<w:OverrideTableStyleHps/>\n++</w:Compatibility>\n++<m:mathPr>\n+++<m:mathFont+m:val=\""Cambria+Math\""/>\n+++<m:brkBin+m:val=\""before\""/>\n+++<m:brkBinSub+m:val=\""&#45;-\""/>\n+++<m:smallFrac+m:val=\""off\""/>\n+++<m:dispDef/>\n+++<m:lMargin+m:val=\""0\""/>\n+++<m:rMargin+m:val=\""0\""/>\n+++<m:defJc+m:val=\""centerGroup\""/>\n+++<m:wrapIndent+m:val=\""1440\""/>\n+++<m:intLim+m:val=\""subSup\""/>\n+++<m:naryLim+m:val=\""undOvr\""/>\n++</m:mathPr></w:WordDocument>\n</xml><![endif]--><!--[if+gte+mso+9]><xml>\n+<w:LatentStyles+DefLockedState=\""false\""+DefUnhideWhenUsed=\""false\""\n++DefSemiHidden=\""false\""+DefQFormat=\""false\""+DefPriority=\""99\""\n++LatentStyleCount=\""376\"">\n++<w:LsdException+Locked=\""false\""+Priority=\""0\""+QFormat=\""true\""+Name=\""Normal\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""9\""+QFormat=\""true\""+Name=\""heading+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""9\""+SemiHidden=\""true\""\n+++UnhideWhenUsed=\""true\""+QFormat=\""true\""+Name=\""heading+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""9\""+SemiHidden=\""true\""\n+++UnhideWhenUsed=\""true\""+QFormat=\""true\""+Name=\""heading+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""9\""+SemiHidden=\""true\""\n+++UnhideWhenUsed=\""true\""+QFormat=\""true\""+Name=\""heading+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""9\""+SemiHidden=\""true\""\n+++UnhideWhenUsed=\""true\""+QFormat=\""true\""+Name=\""heading+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""9\""+SemiHidden=\""true\""\n+++UnhideWhenUsed=\""true\""+QFormat=\""true\""+Name=\""heading+6\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""9\""+SemiHidden=\""true\""\n+++UnhideWhenUsed=\""true\""+QFormat=\""true\""+Name=\""heading+7\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""9\""+SemiHidden=\""true\""\n+++UnhideWhenUsed=\""true\""+QFormat=\""true\""+Name=\""heading+8\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""9\""+SemiHidden=\""true\""\n+++UnhideWhenUsed=\""true\""+QFormat=\""true\""+Name=\""heading+9\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""index+1\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""index+2\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""index+3\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""index+4\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""index+5\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""index+6\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""index+7\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""index+8\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""index+9\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""39\""+SemiHidden=\""true\""\n+++UnhideWhenUsed=\""true\""+Name=\""toc+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""39\""+SemiHidden=\""true\""\n+++UnhideWhenUsed=\""true\""+Name=\""toc+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""39\""+SemiHidden=\""true\""\n+++UnhideWhenUsed=\""true\""+Name=\""toc+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""39\""+SemiHidden=\""true\""\n+++UnhideWhenUsed=\""true\""+Name=\""toc+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""39\""+SemiHidden=\""true\""\n+++UnhideWhenUsed=\""true\""+Name=\""toc+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""39\""+SemiHidden=\""true\""\n+++UnhideWhenUsed=\""true\""+Name=\""toc+6\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""39\""+SemiHidden=\""true\""\n+++UnhideWhenUsed=\""true\""+Name=\""toc+7\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""39\""+SemiHidden=\""true\""\n+++UnhideWhenUsed=\""true\""+Name=\""toc+8\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""39\""+SemiHidden=\""true\""\n+++UnhideWhenUsed=\""true\""+Name=\""toc+9\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Normal+Indent\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""footnote+text\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""annotation+text\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""header\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""footer\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""index+heading\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""35\""+SemiHidden=\""true\""\n+++UnhideWhenUsed=\""true\""+QFormat=\""true\""+Name=\""caption\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""table+of+figures\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""envelope+address\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""envelope+return\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""footnote+reference\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""annotation+reference\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""line+number\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""page+number\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""endnote+reference\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""endnote+text\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""table+of+authorities\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""macro\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""toa+heading\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""List\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""List+Bullet\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""List+Number\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""List+2\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""List+3\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""List+4\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""List+5\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""List+Bullet+2\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""List+Bullet+3\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""List+Bullet+4\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""List+Bullet+5\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""List+Number+2\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""List+Number+3\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""List+Number+4\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""List+Number+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""10\""+QFormat=\""true\""+Name=\""Title\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Closing\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Signature\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""1\""+SemiHidden=\""true\""\n+++UnhideWhenUsed=\""true\""+Name=\""Default+Paragraph+Font\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Body+Text\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Body+Text+Indent\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""List+Continue\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""List+Continue+2\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""List+Continue+3\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""List+Continue+4\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""List+Continue+5\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Message+Header\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""11\""+QFormat=\""true\""+Name=\""Subtitle\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Salutation\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Date\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Body+Text+First+Indent\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Body+Text+First+Indent+2\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Note+Heading\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Body+Text+2\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Body+Text+3\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Body+Text+Indent+2\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Body+Text+Indent+3\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Block+Text\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Hyperlink\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""FollowedHyperlink\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""22\""+QFormat=\""true\""+Name=\""Strong\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""20\""+QFormat=\""true\""+Name=\""Emphasis\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Document+Map\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Plain+Text\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""E-mail+Signature\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""HTML+Top+of+Form\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""HTML+Bottom+of+Form\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Normal+(Web)\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""HTML+Acronym\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""HTML+Address\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""HTML+Cite\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""HTML+Code\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""HTML+Definition\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""HTML+Keyboard\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""HTML+Preformatted\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""HTML+Sample\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""HTML+Typewriter\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""HTML+Variable\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Normal+Table\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""annotation+subject\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""No+List\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Outline+List+1\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Outline+List+2\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Outline+List+3\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Simple+1\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Simple+2\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Simple+3\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Classic+1\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Classic+2\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Classic+3\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Classic+4\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Colorful+1\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Colorful+2\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Colorful+3\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Columns+1\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Columns+2\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Columns+3\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Columns+4\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Columns+5\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Grid+1\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Grid+2\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Grid+3\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Grid+4\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Grid+5\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Grid+6\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Grid+7\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Grid+8\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+List+1\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+List+2\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+List+3\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+List+4\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+List+5\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+List+6\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+List+7\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+List+8\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+3D+effects+1\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+3D+effects+2\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+3D+effects+3\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Contemporary\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Elegant\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Professional\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Subtle+1\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Subtle+2\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Web+1\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Web+2\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Web+3\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Balloon+Text\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""39\""+Name=\""Table+Grid\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Table+Theme\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+Name=\""Placeholder+Text\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""1\""+QFormat=\""true\""+Name=\""No+Spacing\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""60\""+Name=\""Light+Shading\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""61\""+Name=\""Light+List\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""62\""+Name=\""Light+Grid\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""63\""+Name=\""Medium+Shading+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""64\""+Name=\""Medium+Shading+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""65\""+Name=\""Medium+List+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""66\""+Name=\""Medium+List+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""67\""+Name=\""Medium+Grid+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""68\""+Name=\""Medium+Grid+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""69\""+Name=\""Medium+Grid+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""70\""+Name=\""Dark+List\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""71\""+Name=\""Colorful+Shading\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""72\""+Name=\""Colorful+List\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""73\""+Name=\""Colorful+Grid\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""60\""+Name=\""Light+Shading+Accent+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""61\""+Name=\""Light+List+Accent+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""62\""+Name=\""Light+Grid+Accent+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""63\""+Name=\""Medium+Shading+1+Accent+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""64\""+Name=\""Medium+Shading+2+Accent+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""65\""+Name=\""Medium+List+1+Accent+1\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+Name=\""Revision\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""34\""+QFormat=\""true\""\n+++Name=\""List+Paragraph\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""29\""+QFormat=\""true\""+Name=\""Quote\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""30\""+QFormat=\""true\""\n+++Name=\""Intense+Quote\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""66\""+Name=\""Medium+List+2+Accent+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""67\""+Name=\""Medium+Grid+1+Accent+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""68\""+Name=\""Medium+Grid+2+Accent+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""69\""+Name=\""Medium+Grid+3+Accent+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""70\""+Name=\""Dark+List+Accent+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""71\""+Name=\""Colorful+Shading+Accent+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""72\""+Name=\""Colorful+List+Accent+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""73\""+Name=\""Colorful+Grid+Accent+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""60\""+Name=\""Light+Shading+Accent+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""61\""+Name=\""Light+List+Accent+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""62\""+Name=\""Light+Grid+Accent+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""63\""+Name=\""Medium+Shading+1+Accent+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""64\""+Name=\""Medium+Shading+2+Accent+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""65\""+Name=\""Medium+List+1+Accent+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""66\""+Name=\""Medium+List+2+Accent+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""67\""+Name=\""Medium+Grid+1+Accent+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""68\""+Name=\""Medium+Grid+2+Accent+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""69\""+Name=\""Medium+Grid+3+Accent+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""70\""+Name=\""Dark+List+Accent+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""71\""+Name=\""Colorful+Shading+Accent+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""72\""+Name=\""Colorful+List+Accent+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""73\""+Name=\""Colorful+Grid+Accent+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""60\""+Name=\""Light+Shading+Accent+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""61\""+Name=\""Light+List+Accent+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""62\""+Name=\""Light+Grid+Accent+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""63\""+Name=\""Medium+Shading+1+Accent+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""64\""+Name=\""Medium+Shading+2+Accent+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""65\""+Name=\""Medium+List+1+Accent+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""66\""+Name=\""Medium+List+2+Accent+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""67\""+Name=\""Medium+Grid+1+Accent+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""68\""+Name=\""Medium+Grid+2+Accent+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""69\""+Name=\""Medium+Grid+3+Accent+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""70\""+Name=\""Dark+List+Accent+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""71\""+Name=\""Colorful+Shading+Accent+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""72\""+Name=\""Colorful+List+Accent+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""73\""+Name=\""Colorful+Grid+Accent+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""60\""+Name=\""Light+Shading+Accent+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""61\""+Name=\""Light+List+Accent+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""62\""+Name=\""Light+Grid+Accent+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""63\""+Name=\""Medium+Shading+1+Accent+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""64\""+Name=\""Medium+Shading+2+Accent+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""65\""+Name=\""Medium+List+1+Accent+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""66\""+Name=\""Medium+List+2+Accent+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""67\""+Name=\""Medium+Grid+1+Accent+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""68\""+Name=\""Medium+Grid+2+Accent+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""69\""+Name=\""Medium+Grid+3+Accent+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""70\""+Name=\""Dark+List+Accent+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""71\""+Name=\""Colorful+Shading+Accent+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""72\""+Name=\""Colorful+List+Accent+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""73\""+Name=\""Colorful+Grid+Accent+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""60\""+Name=\""Light+Shading+Accent+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""61\""+Name=\""Light+List+Accent+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""62\""+Name=\""Light+Grid+Accent+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""63\""+Name=\""Medium+Shading+1+Accent+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""64\""+Name=\""Medium+Shading+2+Accent+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""65\""+Name=\""Medium+List+1+Accent+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""66\""+Name=\""Medium+List+2+Accent+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""67\""+Name=\""Medium+Grid+1+Accent+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""68\""+Name=\""Medium+Grid+2+Accent+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""69\""+Name=\""Medium+Grid+3+Accent+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""70\""+Name=\""Dark+List+Accent+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""71\""+Name=\""Colorful+Shading+Accent+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""72\""+Name=\""Colorful+List+Accent+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""73\""+Name=\""Colorful+Grid+Accent+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""60\""+Name=\""Light+Shading+Accent+6\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""61\""+Name=\""Light+List+Accent+6\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""62\""+Name=\""Light+Grid+Accent+6\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""63\""+Name=\""Medium+Shading+1+Accent+6\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""64\""+Name=\""Medium+Shading+2+Accent+6\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""65\""+Name=\""Medium+List+1+Accent+6\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""66\""+Name=\""Medium+List+2+Accent+6\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""67\""+Name=\""Medium+Grid+1+Accent+6\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""68\""+Name=\""Medium+Grid+2+Accent+6\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""69\""+Name=\""Medium+Grid+3+Accent+6\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""70\""+Name=\""Dark+List+Accent+6\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""71\""+Name=\""Colorful+Shading+Accent+6\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""72\""+Name=\""Colorful+List+Accent+6\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""73\""+Name=\""Colorful+Grid+Accent+6\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""19\""+QFormat=\""true\""\n+++Name=\""Subtle+Emphasis\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""21\""+QFormat=\""true\""\n+++Name=\""Intense+Emphasis\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""31\""+QFormat=\""true\""\n+++Name=\""Subtle+Reference\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""32\""+QFormat=\""true\""\n+++Name=\""Intense+Reference\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""33\""+QFormat=\""true\""+Name=\""Book+Title\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""37\""+SemiHidden=\""true\""\n+++UnhideWhenUsed=\""true\""+Name=\""Bibliography\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""39\""+SemiHidden=\""true\""\n+++UnhideWhenUsed=\""true\""+QFormat=\""true\""+Name=\""TOC+Heading\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""41\""+Name=\""Plain+Table+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""42\""+Name=\""Plain+Table+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""43\""+Name=\""Plain+Table+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""44\""+Name=\""Plain+Table+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""45\""+Name=\""Plain+Table+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""40\""+Name=\""Grid+Table+Light\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""46\""+Name=\""Grid+Table+1+Light\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""47\""+Name=\""Grid+Table+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""48\""+Name=\""Grid+Table+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""49\""+Name=\""Grid+Table+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""50\""+Name=\""Grid+Table+5+Dark\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""51\""+Name=\""Grid+Table+6+Colorful\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""52\""+Name=\""Grid+Table+7+Colorful\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""46\""\n+++Name=\""Grid+Table+1+Light+Accent+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""47\""+Name=\""Grid+Table+2+Accent+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""48\""+Name=\""Grid+Table+3+Accent+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""49\""+Name=\""Grid+Table+4+Accent+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""50\""+Name=\""Grid+Table+5+Dark+Accent+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""51\""\n+++Name=\""Grid+Table+6+Colorful+Accent+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""52\""\n+++Name=\""Grid+Table+7+Colorful+Accent+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""46\""\n+++Name=\""Grid+Table+1+Light+Accent+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""47\""+Name=\""Grid+Table+2+Accent+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""48\""+Name=\""Grid+Table+3+Accent+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""49\""+Name=\""Grid+Table+4+Accent+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""50\""+Name=\""Grid+Table+5+Dark+Accent+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""51\""\n+++Name=\""Grid+Table+6+Colorful+Accent+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""52\""\n+++Name=\""Grid+Table+7+Colorful+Accent+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""46\""\n+++Name=\""Grid+Table+1+Light+Accent+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""47\""+Name=\""Grid+Table+2+Accent+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""48\""+Name=\""Grid+Table+3+Accent+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""49\""+Name=\""Grid+Table+4+Accent+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""50\""+Name=\""Grid+Table+5+Dark+Accent+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""51\""\n+++Name=\""Grid+Table+6+Colorful+Accent+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""52\""\n+++Name=\""Grid+Table+7+Colorful+Accent+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""46\""\n+++Name=\""Grid+Table+1+Light+Accent+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""47\""+Name=\""Grid+Table+2+Accent+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""48\""+Name=\""Grid+Table+3+Accent+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""49\""+Name=\""Grid+Table+4+Accent+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""50\""+Name=\""Grid+Table+5+Dark+Accent+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""51\""\n+++Name=\""Grid+Table+6+Colorful+Accent+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""52\""\n+++Name=\""Grid+Table+7+Colorful+Accent+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""46\""\n+++Name=\""Grid+Table+1+Light+Accent+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""47\""+Name=\""Grid+Table+2+Accent+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""48\""+Name=\""Grid+Table+3+Accent+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""49\""+Name=\""Grid+Table+4+Accent+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""50\""+Name=\""Grid+Table+5+Dark+Accent+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""51\""\n+++Name=\""Grid+Table+6+Colorful+Accent+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""52\""\n+++Name=\""Grid+Table+7+Colorful+Accent+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""46\""\n+++Name=\""Grid+Table+1+Light+Accent+6\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""47\""+Name=\""Grid+Table+2+Accent+6\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""48\""+Name=\""Grid+Table+3+Accent+6\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""49\""+Name=\""Grid+Table+4+Accent+6\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""50\""+Name=\""Grid+Table+5+Dark+Accent+6\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""51\""\n+++Name=\""Grid+Table+6+Colorful+Accent+6\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""52\""\n+++Name=\""Grid+Table+7+Colorful+Accent+6\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""46\""+Name=\""List+Table+1+Light\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""47\""+Name=\""List+Table+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""48\""+Name=\""List+Table+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""49\""+Name=\""List+Table+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""50\""+Name=\""List+Table+5+Dark\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""51\""+Name=\""List+Table+6+Colorful\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""52\""+Name=\""List+Table+7+Colorful\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""46\""\n+++Name=\""List+Table+1+Light+Accent+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""47\""+Name=\""List+Table+2+Accent+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""48\""+Name=\""List+Table+3+Accent+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""49\""+Name=\""List+Table+4+Accent+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""50\""+Name=\""List+Table+5+Dark+Accent+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""51\""\n+++Name=\""List+Table+6+Colorful+Accent+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""52\""\n+++Name=\""List+Table+7+Colorful+Accent+1\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""46\""\n+++Name=\""List+Table+1+Light+Accent+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""47\""+Name=\""List+Table+2+Accent+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""48\""+Name=\""List+Table+3+Accent+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""49\""+Name=\""List+Table+4+Accent+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""50\""+Name=\""List+Table+5+Dark+Accent+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""51\""\n+++Name=\""List+Table+6+Colorful+Accent+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""52\""\n+++Name=\""List+Table+7+Colorful+Accent+2\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""46\""\n+++Name=\""List+Table+1+Light+Accent+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""47\""+Name=\""List+Table+2+Accent+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""48\""+Name=\""List+Table+3+Accent+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""49\""+Name=\""List+Table+4+Accent+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""50\""+Name=\""List+Table+5+Dark+Accent+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""51\""\n+++Name=\""List+Table+6+Colorful+Accent+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""52\""\n+++Name=\""List+Table+7+Colorful+Accent+3\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""46\""\n+++Name=\""List+Table+1+Light+Accent+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""47\""+Name=\""List+Table+2+Accent+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""48\""+Name=\""List+Table+3+Accent+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""49\""+Name=\""List+Table+4+Accent+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""50\""+Name=\""List+Table+5+Dark+Accent+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""51\""\n+++Name=\""List+Table+6+Colorful+Accent+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""52\""\n+++Name=\""List+Table+7+Colorful+Accent+4\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""46\""\n+++Name=\""List+Table+1+Light+Accent+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""47\""+Name=\""List+Table+2+Accent+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""48\""+Name=\""List+Table+3+Accent+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""49\""+Name=\""List+Table+4+Accent+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""50\""+Name=\""List+Table+5+Dark+Accent+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""51\""\n+++Name=\""List+Table+6+Colorful+Accent+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""52\""\n+++Name=\""List+Table+7+Colorful+Accent+5\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""46\""\n+++Name=\""List+Table+1+Light+Accent+6\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""47\""+Name=\""List+Table+2+Accent+6\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""48\""+Name=\""List+Table+3+Accent+6\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""49\""+Name=\""List+Table+4+Accent+6\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""50\""+Name=\""List+Table+5+Dark+Accent+6\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""51\""\n+++Name=\""List+Table+6+Colorful+Accent+6\""/>\n++<w:LsdException+Locked=\""false\""+Priority=\""52\""\n+++Name=\""List+Table+7+Colorful+Accent+6\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Mention\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Smart+Hyperlink\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Hashtag\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Unresolved+Mention\""/>\n++<w:LsdException+Locked=\""false\""+SemiHidden=\""true\""+UnhideWhenUsed=\""true\""\n+++Name=\""Smart+Link\""/>\n+</w:LatentStyles>\n</xml><![endif]--><!--[if+gte+mso+10]>\n<style>\n+/*+Style+Definitions+*/\n+table.MsoNormalTable\n\t{{mso-style-name:Standardowy;\n\tmso-tstyle-rowband-size:0;\n\tmso-tstyle-colband-size:0;\n\tmso-style-noshow:yes;\n\tmso-style-priority:99;\n\tmso-style-parent:\""\"";\n\tmso-padding-alt:0cm+5.4pt+0cm+5.4pt;\n\tmso-para-margin:0cm;\n\tmso-pagination:widow-orphan;\n\tfont-size:10.0pt;\n\tfont-family:\""Times+New+Roman\"",serif;}}\n</style>\n<![endif]--></p><p><!--[if+gte+mso+10]>\n<style>\n+/*+Style+Definitions+*/\n+table.MsoNormalTable\n\t{{mso-style-name:Standardowy;\n\tmso-tstyle-rowband-size:0;\n\tmso-tstyle-colband-size:0;\n\tmso-style-noshow:yes;\n\tmso-style-priority:99;\n\tmso-style-parent:\""\"";\n\tmso-padding-alt:0cm+5.4pt+0cm+5.4pt;\n\tmso-para-margin:0cm;\n\tmso-pagination:widow-orphan;\n\tfont-size:10.0pt;\n\tfont-family:\""Times+New+Roman\"",serif;}}\n</style>\n<![endif]--></p><p>Przyjmowanie+zgłoszeń+na+złożenie+wniosku+o+pobyt+czasowy+w+sesji+z+16.09.2024+zostało+zakończone.</p><p><!--[if+gte+mso+10]>\n<style>\n+/*+Style+Definitions+*/\n+table.MsoNormalTable\n\t{{mso-style-name:Standardowy;\n\tmso-tstyle-rowband-size:0;\n\tmso-tstyle-colband-size:0;\n\tmso-style-noshow:yes;\n\tmso-style-priority:99;\n\tmso-style-parent:\""\"";\n\tmso-padding-alt:0cm+5.4pt+0cm+5.4pt;\n\tmso-para-margin:0cm;\n\tmso-pagination:widow-orphan;\n\tfont-size:10.0pt;\n\tfont-family:\""Times+New+Roman\"",serif;}}\n</style>\n<![endif]-->Zachęcamy+do+skorzystania+z+zapisów+poprzez+formularz+zgłoszeniowy+w+kolejnym+terminie.</p><p>Termin+aktywacji+kolejnej+sesji+formularza+zgłoszeniowego+dostępny+będzie+na+stronie+migrant.wsc.mazowieckie.pl.<br><br><span+style=\""font-family:&quot;Calibri&quot;,sans-serif;color:black\""></span></p><p></p><p>\n\n<br></p>"",
        //      ""calculations"":[

        //      ],
        //      ""header_position"":""left"",
        //      ""footer_position"":""left"",
        //      ""save_progress_allow_multiple"":"""",
        //      ""save_progress_table_legend"":""Load+saved+progress"",
        //      ""save_progress_table_columns"":[
        //         {{
        //            ""errors"":[

        //            ],
        //            ""max_options"":0,
        //            ""label"":""Column+Title"",
        //            ""field"":""{{field}}"",
        //            ""order"":0,
        //            ""settingModel"":{{
        //               ""settings"":false,
        //               ""hide_merge_tags"":false,
        //               ""error"":false,
        //               ""name"":""save_progress_table_columns"",
        //               ""type"":""option-repeater"",
        //               ""label"":""Save+Table+Columns+<a+href=\""#\""+class=\""nf-add-new\"">Add+New</a>"",
        //               ""width"":""full"",
        //               ""group"":""primary"",
        //               ""columns"":{{
        //                  ""field"":{{
        //                     ""header"":""Field+Key"",
        //                     ""default"":""""
        //                  }}
        //               }},
        //               ""value"":[
        //                  {{
        //                     ""label"":""Column+Title"",
        //                     ""field"":""{{field}}"",
        //                     ""order"":0
        //                  }}
        //               ],
        //               ""tmpl_row"":""tmpl-nf-save-progress-table-columns-repeater-row""
        //            }}
        //         }}
        //      ],
        //      ""container_styles_show_advanced_css"":""0"",
        //      ""title_styles_show_advanced_css"":""0"",
        //      ""row_styles_show_advanced_css"":""0"",
        //      ""row-odd_styles_show_advanced_css"":""0"",
        //      ""success-msg_styles_show_advanced_css"":""0"",
        //      ""error_msg_styles_show_advanced_css"":""0"",
        //      ""conditions"":[

        //      ],
        //      ""changeEmailErrorMsg"":""Wprowadź+prawidłowy+adres+e-mail!"",
        //      ""changeDateErrorMsg"":""Please+enter+a+valid+date!"",
        //      ""confirmFieldErrorMsg"":""Te+pola+muszą+się+zgadzać!"",
        //      ""fieldNumberNumMinError"":""Błąd+—+liczba+minimalna"",
        //      ""fieldNumberNumMaxError"":""Błąd+—+liczba+maksymalna"",
        //      ""fieldNumberIncrementBy"":""Zwiększ+o+"",
        //      ""formErrorsCorrectErrors"":""Popraw+błędy,+zanim+prześlesz+formularz."",
        //      ""validateRequiredField"":""To+pole+jest+wymagane"",
        //      ""honeypotHoneypotError"":""Błąd+Honeypot"",
        //      ""fieldsMarkedRequired"":""Pola+oznaczone+znakiem+<span+class=\""ninja-forms-req-symbol\"">*</span>+są+wymagane"",
        //      ""drawerDisabled"":false,
        //      ""container_styles_border"":"""",
        //      ""container_styles_height"":"""",
        //      ""container_styles_margin"":"""",
        //      ""container_styles_padding"":"""",
        //      ""container_styles_float"":"""",
        //      ""title_styles_border"":"""",
        //      ""title_styles_height"":"""",
        //      ""title_styles_width"":"""",
        //      ""title_styles_font-size"":"""",
        //      ""title_styles_margin"":"""",
        //      ""title_styles_padding"":"""",
        //      ""title_styles_float"":"""",
        //      ""row_styles_border"":"""",
        //      ""row_styles_width"":"""",
        //      ""row_styles_font-size"":"""",
        //      ""row_styles_margin"":"""",
        //      ""row_styles_padding"":"""",
        //      ""row-odd_styles_border"":"""",
        //      ""row-odd_styles_height"":"""",
        //      ""row-odd_styles_width"":"""",
        //      ""row-odd_styles_font-size"":"""",
        //      ""row-odd_styles_margin"":"""",
        //      ""row-odd_styles_padding"":"""",
        //      ""success-msg_styles_border"":"""",
        //      ""success-msg_styles_height"":"""",
        //      ""success-msg_styles_width"":"""",
        //      ""success-msg_styles_font-size"":"""",
        //      ""success-msg_styles_margin"":"""",
        //      ""success-msg_styles_padding"":"""",
        //      ""error_msg_styles_border"":"""",
        //      ""error_msg_styles_width"":"""",
        //      ""error_msg_styles_font-size"":"""",
        //      ""error_msg_styles_margin"":"""",
        //      ""error_msg_styles_padding"":"""",
        //      ""allow_public_link"":0,
        //      ""embed_form"":"""",
        //      ""sub_limit_number"":""2000"",
        //      ""ninjaForms"":""Ninja+Forms"",
        //      ""fieldTextareaRTEInsertLink"":""Wstaw+łącze"",
        //      ""fieldTextareaRTEInsertMedia"":""Wstaw+multimedia"",
        //      ""fieldTextareaRTESelectAFile"":""Wybierz+plik"",
        //      ""formHoneypot"":""Jeśli+jesteś+człowiekiem+i+widzisz+to+pole,+nie+wypełniaj+go."",
        //      ""fileUploadOldCodeFileUploadInProgress"":""Przesyłanie+pliku+w+toku."",
        //      ""fileUploadOldCodeFileUpload"":""PRZESYŁANIE+PLIKU"",
        //      ""currencySymbol"":""&#36;"",
        //      ""thousands_sep"":""&nbsp;"",
        //      ""decimal_point"":"","",
        //      ""siteLocale"":""pl_PL"",
        //      ""dateFormat"":""m/d/Y"",
        //      ""startOfWeek"":""1"",
        //      ""of"":""z"",
        //      ""previousMonth"":""Poprzedni+miesiąc"",
        //      ""nextMonth"":""Następny+miesiąc"",
        //      ""months"":[
        //         ""styczeń"",
        //         ""luty"",
        //         ""marzec"",
        //         ""kwiecień"",
        //         ""maj"",
        //         ""czerwiec"",
        //         ""lipiec"",
        //         ""sierpień"",
        //         ""wrzesień"",
        //         ""październik"",
        //         ""listopad"",
        //         ""grudzień""
        //      ],
        //      ""monthsShort"":[
        //         ""sty"",
        //         ""lut"",
        //         ""mrz"",
        //         ""kw"",
        //         ""maj"",
        //         ""cze"",
        //         ""lip"",
        //         ""sie"",
        //         ""wrz"",
        //         ""paź"",
        //         ""lis"",
        //         ""gru""
        //      ],
        //      ""weekdays"":[
        //         ""niedziela"",
        //         ""poniedziałek"",
        //         ""wtorek"",
        //         ""środa"",
        //         ""czwartek"",
        //         ""piątek"",
        //         ""sobota""
        //      ],
        //      ""weekdaysShort"":[
        //         ""niedz."",
        //         ""pon."",
        //         ""wt."",
        //         ""śr."",
        //         ""czw."",
        //         ""pt."",
        //         ""sob.""
        //      ],
        //      ""weekdaysMin"":[
        //         ""niedz."",
        //         ""pon."",
        //         ""wt."",
        //         ""śr."",
        //         ""czw."",
        //         ""pt."",
        //         ""sob.""
        //      ],
        //      ""currency_symbol"":"""",
        //      ""beforeForm"":"""",
        //      ""beforeFields"":"""",
        //      ""afterFields"":"""",
        //      ""afterForm"":""""
        //   }},
        //   ""extra"":{{

        //   }}
        //}}";

        //            req.Content = new FormUrlEncodedContent(new Dictionary<string, string>
        //            {
        //                { "action", "nf_ajax_submit" },
        //                { "security", data.Nonce},
        //                { "formData", fromData }
        //            });
        //            var res = await client.SendAsync(req);
        //            var resData = await res.Content.ReadAsStringAsync();

        //            log("Odpowiedz: " + resData);
        //        }

        //    }

    }
}
