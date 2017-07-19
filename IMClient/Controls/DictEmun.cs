using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMClient.Controls
{
    public enum ImageBtnKey { 
        /// <summary>
        /// 搜索按钮
        /// </summary>
         imgDatabase,
        /// <summary>
        /// 工作台
        /// </summary>
        imgWorkTable
    }

    #region
    /// <summary>
    /// 页面操作方法
    /// </summary>
    public enum TFunction
    {
        add=0,modify=1
    }
    #endregion

    /// <summary>
    /// 鼠标状态
    /// </summary>
    public enum MouseStatus
    { 
      normal,over,press
    }


    #region 用户角色变化枚举
    /// <summary>
    /// 用户角色1
    /// </summary>
    public enum UserRole
    {
        Admin = -1,
        Exaperson = 0x195,
        Other = 0,
        SampleCollector = 0x103,
        SamplingUnit = 140,
        Techdirector = 0x1ff,
        Techperson = 0x1bf
    }

    /// <summary>
    /// 用户角色2
    /// </summary>
    public enum Userpop
    {
        Admin = 1,
        Exaperson = 3,
        Other = 0x63,
        SampleCollector = 6,
        SamplingUnit = 4,
        Techdirector = 2,
        Techperson = 5
    }
    #endregion

    public enum lineType { 
        /// <summary>
        /// 实线
        /// </summary>
        solid,
        /// <summary>
        /// 虚线
        /// </summary>
        spash
    }

    /// <summary>
    /// 线的位置
    /// </summary>
    public enum lineLocationType
    { 
       near,center,Far
    }

    /// <summary>
    /// 字典类型Category
    /// </summary>
    public enum DictEmun
    {
        /// <summary>
        /// 个体状态
        /// </summary>
        SingleStatus = 89,

        /// <summary>
        /// 个体类型
        /// </summary>
        SingleType = 92,

        /// <summary>
        /// 检材类型
        /// </summary>
        SamplesType=93,

        /// <summary>
        /// 系统配置
        /// </summary>
        sysParams=201,

        /// <summary>
        /// 委托类型
        /// </summary>
        commitType = 68,

        /// <summary>
        /// 职业
        /// </summary>
        occupation = 11,
        /// <summary>
        /// 送检单位
        /// </summary>
        sendUnit = 96,

        /// <summary>
        /// 人员性别
        /// </summary>
        sextype = 5,

        /// <summary>
        /// 国家
        /// </summary>
        Nation = 22,

        /// <summary>
        /// 民族
        /// </summary>
        fold = 14,

        /// <summary>
        /// 被检验人员类型
        /// </summary>
        perType = 90,

        /// <summary>
        /// 证件类型
        /// </summary>
        idType = 45,

        /// <summary>
        /// 样品类型
        /// </summary>
        samplingType = 33,

        /// <summary>
        /// 关押上所
        /// </summary>
        custodyPlace = 77,

        /// <summary>
        /// 样品柜
        /// </summary>
        sampleCabinet = 85,

        /// <summary>
        /// 案件级别
        /// </summary>
        caseGrade = 37,

        /// <summary>
        /// 案件类别
        /// </summary>
        caseType = 32,

        /// <summary>
        /// 对象类别
        /// </summary>
        objectType = 60,

        /// <summary>
        /// 载体类型
        /// </summary>
        carrierType = 95,

        /// <summary>
        /// 试管架类型
        /// </summary>
        tubeRackType = 73,

        /// <summary>
        /// 实验类型
        /// </summary>
        testType = 72,

        /// <summary>
        /// 审核结果
        /// </summary>
        examineResult = 112,

        /// <summary>
        /// 涉案类型
        /// </summary>
        sheCaseType = 32,

        /// <summary>
        /// 失踪原因
        /// </summary>
        lostReason = 91,

        /// <summary>
        /// 发挥作用
        /// </summary>
        acceptOpinion = 94,

        /// <summary>
        /// 样品关系
        /// </summary>
        sampleRelation =51,

        /// <summary>
        /// 分析类型
        /// </summary>
        typeanalysis = 79,

        /// <summary>
        /// 分型结果名称
        /// </summary>
        genoTypeName=34,
        /// <summary>
        /// 比对模式
        /// </summary>
        ComparePattern = 10000,
        /// <summary>
        /// 分局编号
        /// </summary>
        Fenju=11000
    }

    /// <summary>
    /// 进入实验类型
    /// </summary>
    public enum EnterLabType
    {
        /// <summary>
        /// 委托建库批量
        /// </summary>
        CommitDatabaseGroup,

        /// <summary>
        /// 委托建库个别
        /// </summary>
        CommitDatabaseSingle,

        /// <summary>
        /// 委托鉴定
        /// </summary>
        CommitIdentify,

        /// <summary>
        /// 实验查询
        /// </summary>
        Testindex,

        /// <summary>
        /// 任务实验
        /// </summary>
        MissionTestindex
    }


    /// <summary>
    /// 委托建库
    /// </summary>
    public enum CommitFrmType
    { 
        commit,
        per,
        sample
    }


    public enum CompareKey
    { 
        /// <summary>
        /// y-str
        /// </summary>
        FY,
        /// <summary>
        /// 单亲父 形成的数据最后加上DES： “F”/"M"/"S"
        /// </summary>
        FF,
        /// <summary>
        /// 单亲母 形成的数据最后加上DES： “F”/"M"/"S"
        /// </summary>
        FM,
        /// <summary>
        /// 三联体 形成的数据最后加上DES： “F”/"M"/"S"
        /// </summary>
        F3,
        /// <summary>
        /// 三联体 含单亲 形成的数据最后加上DES： “F”/"M"/"S"
        /// </summary>
        FJ3,
        /// <summary>
        /// 单比
        /// </summary>
        CS,
        /// <summary>
        /// 混合
        /// </summary>
        CM,
        /// <summary>
        /// 同胞兄弟 比对
        /// </summary>
        FC,


         /// <summary>
        /// y-str
        /// </summary>
        DFY,
        /// <summary>
        /// 单亲父 形成的数据最后加上DES： “F”/"M"/"S"
        /// </summary>
        DFF,
        /// <summary>
        /// 单亲母 形成的数据最后加上DES： “F”/"M"/"S"
        /// </summary>
        DFM,
        /// <summary>
        /// 三联体 形成的数据最后加上DES： “F”/"M"/"S"
        /// </summary>
        DF3,
        /// <summary>
        /// 三联体 含单亲 形成的数据最后加上DES： “F”/"M"/"S"
        /// </summary>
        DFJ3,
        /// <summary>
        /// 单比
        /// </summary>
        DCS,
        /// <summary>
        /// 混合
        /// </summary>
        DCM,
        /// <summary>
        /// 同胞兄弟 比对
        /// </summary>
        DFC
    }

    /// <summary>
    /// 家庭关系
    /// </summary>
    public enum family {
        farther = 0, mother=1,child=2
    }

    public enum CompareRe { 
        /// <summary>
        /// 系统错误
        /// </summary>
        systemError=-1,

        /// <summary>
        /// 没有比中
        /// </summary>
        none=0,

        /// <summary>
        /// 比对成功
        /// </summary>
        success
    }

    /// <summary>
    /// 对象、样品关系属性
    /// </summary>
    public enum Rel
    {
        Auto = 0,
        /// <summary>
        /// 兄妹
        /// </summary>
        BrotherSister = 6,
        /// <summary>
        /// 孩子
        /// </summary>
        Child = 5,
        /// <summary>
        /// 祖父
        /// </summary>
        Fa = 8,
        /// <summary>
        /// 父亲
        /// </summary>
        Father = 1,
        /// <summary>
        /// 丈夫
        /// </summary>
        Husband = 3,
        /// <summary>
        /// 
        /// </summary>
        Independent = 0,
        /// <summary>
        /// 祖母
        /// </summary>
        Ma = 9,
        /// <summary>
        /// 母亲
        /// </summary>
        Mother = 2,
        /// <summary>
        /// 自己
        /// </summary>
        Self = 0,
        /// <summary>
        /// 未知对象
        /// </summary>
        UnidentifiedPerson = 10,
        /// <summary>
        /// 妻子
        /// </summary>
        Wife = 4
    }

    /*
    失踪人口= 7,
    自动 = 0,
    基础数据= 8,
    犯罪人员 = 3,
    鉴定= 1,
    案件物证= 5,
    重点人员= 4,
    其他= 9,
    标准样品= 0x62,
    嫌疑人 = 2,
    不明身份= 6

     */
    /// <summary>
    /// 对象类型    dict = 68
    /// </summary>
    public enum PerCategory { 
        /// <summary>
        /// 自动
        /// </summary>
        auto=0,
        /// <summary>
        /// 鉴定（委托鉴定）
        /// </summary>
        identify = 1,
        /// <summary>
        /// 嫌疑人
        /// </summary>
        suspects=2,
        /// <summary>
        /// 犯罪人
        /// </summary>
        criminal=3,       
        /// <summary>
        /// 重点人员（委托建库）
        /// </summary>
        keyPerson=4,
        /// <summary>
        /// 案件物证（委托建库）
        /// </summary>
        caseEvdence = 5,
        /// <summary>
        /// 不明身份（委托建库）
        /// </summary>
        unkown = 6,
        /// <summary>
        /// 失踪人口（委托建库）
        /// </summary>
        lost=7,

        /// <summary>
        /// 基础资料（基础研究）
        /// </summary>
        baseData=8,
        /// <summary>
        /// 其他（其他委托）
        /// </summary>
        other=9,
        /// <summary>
        /// 标准样（实验标准）
        /// </summary>
        sample=0x62,
       
        
    }

    /// <summary>
    /// 审核结果
    /// </summary>
    public enum AcceptType {
        /// <summary>
        /// 无效
        /// </summary>
        Unalbe=0,
        /// <summary>
        /// 串案
        /// </summary>
        Conspiracy=1,
        /// <summary>
        /// 正查
        /// </summary>
        Frontage=2,
        /// <summary>
        /// 倒查
        /// </summary>
        Back=3
    }

    /// <summary>
    /// 比对结果
    /// </summary>
    public enum CompareType {
        /// <summary>
        /// 同一认定
        /// </summary>
        Identification=1,
        /// <summary>
        /// 单亲父
        /// </summary>
        Singlefather=2,
        /// <summary>
        /// 单亲母
        /// </summary>
        Singlemothers=3,
        /// <summary>
        /// 三联体
        /// </summary>
        triplet=4,
        /// <summary>
        /// 综合
        /// </summary>
        synthesize=5
    }

    /// <summary>
    /// 基因 关系 未录入字典
    /// </summary>
    public enum Tgenotype
    {
        MT = 8,
        NotSet = -1,
        SNP = 0x10,
        STR = 1,
        Unknown = 0,
        XSTR = 4,
        YSTR = 2
    }

    /// <summary>
    /// 服务器状体 未录入字典
    /// </summary>
    public enum ServerStatus
    { 
        /// <summary>
        /// 未初始化 不可使用
        /// </summary>
          unInit=0,
         /// <summary>
         /// 正在初始 不可使用
         /// </summary>
          Initing=1,
        /// <summary>
        /// 空闲 可使用
        /// </summary>
          normal=2,
        /// <summary>
        /// 正在比对 可使用
        /// </summary>
          working=3,
        /// <summary>
        /// 插入工作 不可使用
        /// </summary>
          insert=4,
        /// <summary>
        /// 正在更新 不可使用
        /// </summary>
          update=5,
        /// <summary>
        /// 正在维护 不可使用
        /// </summary>
          maintenance=6,
        /// <summary>
        /// 系统退出 不可使用
        /// </summary>
          systemout=7,
        /// <summary>
        /// 其他原因 不可使用
        /// </summary>
          other=8
    }

    /// <summary>
    /// 委托终结状态
    /// </summary>
    public enum Tstatus
    {
        /// <summary>
        /// 未终结
        /// </summary>
        UnFinished, 

        /// <summary>
        /// 已终结
        /// </summary>
        Finished,   

        /// <summary>
        /// 刑事案件已破，相关物证DNA数据已建库，检验终结
        /// </summary>
        CaseCracked, 

        /// <summary>
        /// 刑事案件未破，相关物证DNA数据已建库，检验终结
        /// </summary>
        CaseUncracked, 

        /// <summary>
        /// 不是刑事案件，相关物证DNA数据已建库，检验终结
        /// </summary>
        NotCase,  

        /// <summary>
        /// 范畴不属于建库,检验终结
        /// </summary>
        NotDatabasing 
    }

    public enum UserAccess
    {
        AdminUpdate = 0x40,
        AdminView = 0x80,
        CommitUpdate = 2,
        CommitView = 1,
        InfoUpdate = 8,
        InfoView = 4,
        LimsClient = 0x100,
        TestUpdate = 0x20,
        TestView = 0x10
    }

    public enum TextStatus
    {
        /// <summary>
        /// 不限制
        /// </summary>
        ALL,
        /// <summary>
        /// 只能数字
        /// </summary>
        Number,
        /// <summary>
        /// 只能字母
        /// </summary>
        Letter,
        /// <summary>
        /// 只能数字加字母
        /// </summary>
        NAndL
    }

    public enum Tper
    {
        AbscondenceRelative = 7,
        Auto = 0,
        BasicData = 8,
        Culprit = 3,
        Evidence = 1,
        ImportantCase = 5,
        ImportantPerson = 4,
        Other = 9,
        StdSample = 0x62,
        Suspect = 2,
        UnidetifiedUnit = 6
    }

    /// <summary>
    /// 更新程序类型
    /// </summary>
    public enum TUpdateType
    {
        mainEXE = 1,
        updateExE = 2,
        XML = 3,
        Doc = 4
    }

    /// <summary>
    /// 重点关注模式
    /// </summary>
    public enum ImportModel
    { 
     Normal=0,Import=1
    }

    /// <summary>
    /// 当前数据的类型
    /// </summary>
    public enum DataType
    { 
      commit=0,per=1,sample=2
    }

    /// <summary>
    /// 按钮的用途
    /// </summary>
    public enum btnType
    { 
     add,modify,del
    }
}
