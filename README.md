# Infrastructure
基于.Net开发的常用接口基础框架主体包含三个接口实现完成：配置接口、接入接口、处理接口：

<br>1)IParameter主要定义接口参数相关的处理细节；包括配置参数、查询参数、网络参数等；

<br>2)IAccess主要定义接口接入部分的处理，其中包含IDownload、IUpload两个分支接口；

<br>3)Ihandler主要定义数据实体层面的CRU等处理细节，分别各自对应：IGet、IPost、IPut接口；

<br>4)其他包含了基于SAP接口、Quartz自动定时任务抽取第三方接口等相关配置项

# Request Sample
## 1)HttpRequestSample
<br> HttpParameters p = new HttpParameters();
<br>p.Url = "http://xxxxx/Service.svc/Customer/GetCustomerChain";
<br>p.Method = RequestMethod.Post;
<br>p.ContentType = ContentType.Json;
<br>p.ParameterValue = "";

<br>string result = InterfaceHttpRequest.Query(p);
## 2)InterfaceRequestSample
<br>int sequence = (int)ServiceName.UserService;
<br>string url = (string)InterfaceParameter.Instance.GetConfigParameters()["POS"];
<br>string[] p = new string[] { url, string.Empty };
<br>HttpParameters query = InterfaceParameter.Instance.GetHttpParameters(sequence, p);
<br>InterfaceRequest request = new InterfaceRequest();
<br>request.Sequence = sequence;
<br>request.Parameter.Add(query);
<br>InterfaceResult<UserEntity> result = InterfaceHandler.Instance.GetEntities<UserEntity>(request);
<br>if (result.Status == Status.Failed) InterfaceHandler.Instance.Throw();
## 3)HttpSoapSample
 <br>//获取配置参数
 <br>string[] p = new string[] { (string)InterfaceParameter.Instance.GetConfigParameters()["POS"]
                                 ,string.Empty
                                 ,System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd")};

<br>//创建Soap实体对象并添加参数
<br>SoapParameter soap = new SoapParameter();
<br>StringBuilder sb = new StringBuilder();
 
<br>//添加根节点
<br>soap.RootNode = "soapenv";
<br>soap.RootDefaultNameSpace = "xmlns:ws=\"http://ws.nip.com\"";
<br>soap.SoapAction = "";

<br>//添加函数API
<br>soap.MethodNode = "ws:shopSystemInspectData";
<br>soap.MethodDefaultNameSpace = "soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\"";

<br>//添加参数
<br>sb.AppendFormat("<shopcode xsi:type=\"xsd:string\">{0}</shopcode>", p[1]);
<br>sb.AppendFormat("<idate xsi:type=\"xsd:string\">{0}</idate>", p[2]);
<br>soap.MethodParameterValue = sb.ToString();  

<br>//创建请求对象
<br>HttpParameters query = InterfaceHttpConfig.Setting(p[0], soap.ToString(), DataFormatter.SOAP);

<br>//调用接口获取返回信息；
<br>HttpParameters http = (HttpParameters)query;
<br>XmlDocument doc = InterfaceHttpRequest.QueryXml(http);
