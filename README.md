# Infrastructure
基于.Net开发的常用接口基础框架主体包含两个接口实现完成：配置接口、处理接口：<br> 
1）配置接口主要针对接口配置项信息，如DB连接、接口Url、接口调用参数配置、邮件发送异常等；<br>
2）处理接口主要针对常规增、查、改等操作进行数据推送与抽取处理细节；<br>
3）其他包含了基于SAP接口、Quartz自动定时任务抽取第三方接口等相关配置项<br>

# Request Sample
## HttpRequestSample
<br> HttpParameters p = new HttpParameters();
<br>p.Url = "http://xxxxx/Service.svc/Customer/GetCustomerChain";
<br>p.Method = RequestMethod.Post;
<br>p.ContentType = ContentType.Json;
<br>p.ParameterValue = "";
<br>string result = InterfaceHttpRequest.Query(p);
## InterfaceRequestSample

<br>int sequence = (int)ServiceName.UserService;
<br>string url = (string)InterfaceParameter.Instance.GetConfigParameters()["POS"];

<br>string[] p = new string[] { url, string.Empty };
<br>HttpParameters query = InterfaceParameter.Instance.GetHttpParameters(sequence, p);

 <br>InterfaceRequest request = new InterfaceRequest();
 <br>request.Sequence = sequence;
 <br>request.Parameter.Add(query);

  <br>InterfaceResult<UserEntity> result = InterfaceHandler.Instance.GetEntities<UserEntity>(request);
  <br>if (result.Status == Status.Failed) InterfaceHandler.Instance.Throw();
