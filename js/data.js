const PageData = {
    logoImgUrl:'./assets/img/lightbox.png',
    githubLogo:'./assets/icons/github.svg',
    isWip:true,
    wip:'Work In Progress',
    githubLogoAlt:'github logo',
    repo:'https://github.com/SqlBox/LightBox',
    title:'LightBox',
    titleHeadline:'LightBox IDE',
    titleDescription:'Open source Multi Database SQL client IDE. Focusing on simplicity, speed, small footprint',
    secondDescription:'Lightbox is a light weight and robust frontend for databases. It offers a powerful text editor, a sql console, object browser and a few useful tools. It has small footprint.Lightbox is not comprehensive as MySql-Workbench or PLSQL Developer and never will provide so much functionality but some people like it clean and fast.',
    appScreenShot:'./assets/img/introvid.gif',
    appScreenShotAlt:'app screenshot',
    vid1:'./assets/img/vid1.gif',
    navItems:[
        {title:'Downloads',href:'#downloads'},
        {title:'Feautures', href:'#features'},
        {title:'Apps',href:'#apps'},
        {title:'More',href:'#footer'}
    ],
    footer: {
        content:'Content',
        items:[
            {title:'Firedump',link:'https://github.com/SqlBox/firedump'},
            {title:'Task tracker',link:'https://trello.com/b/hoXLWMX2/lightbox'},
            {title:'SqlStatementParser',link:'https://github.com/SqlBox/SqlStatementParser'}
        ],
        copyright:{
            caption:'© 2020 Copyright:'
        }
    },
    features:{
        title:'Features',
        items:[
            {title:'Intellisense Editor',description:'Schema tables and fields plus common sql commands',img:'./assets/img/vid1.gif',imgAlt:'Intellisense Editor'},
            {title:'Lazy load limit results',description:'Limit the results you want. Auto fast fetch next chunk while scrolling',img:'./assets/img/lazy.gif',imgAlt:'lazy load'},
            {title:'Multi Database support',description:'Databases supported MYSQL, POSTGRESQL, SQLITE, MSSQL, ORACLE, MARIADB, IBM DB2, FIREBIRD',img:'./assets/img/choosedb.png',imgAlt:'multi database support'},
        ],
        extraFeatures:{
            title:'Database vendor support progress',
            featureProgress:[
                {completed:true,title:'MySql'},
                {completed:true,title:'Sqlite'},
                {completed:true,title:'MariaDb'},
                {completed:false,title:'PostgreSql'},
                {completed:false,title:'MSSQL'},
                {completed:false,title:'Oracle'},
                {completed:false,title:'IBM Db2'},
                {completed:false,title:'Firebird'}
            ],
            trello:{
                title:'Progress',
                icon:'./assets/icons/trello-logo-blue.svg',
                url:'https://trello.com/b/hoXLWMX2/lightbox',
                alt:'trello',
            }
        }
    },
    downloads:{
        title:'Downloads',
        description:'Downloads are available for 32bit and 64bit executables.',
        requirements:'Minimum requirement to run the app is to have installed .NET 4.6.1 and above',
        links:[
            {title:'DebugX64 exe',link:'https://github.com/SqlBox/LightBox/releases/download/demo-29-10-20/lightbox.zip',active:true},
            {title:'DebugX86 exe',link:'https://github.com/SqlBox/LightBox/releases/download/debug64/debug64.zip',active:false}
        ]
    },
    apps:{
        title:'Other apps and tools',
        tools:[
            {title:'Firedump',imgAlt:'firedump pic',img:'./assets/img/firedump.png',description:'MySql  backup...and even more .net desktop app',url:'https://github.com/SqlBox/firedump'},
            {title:'SqlStatementParser',description:'A multy database sql statement parser in .net core',url:'https://github.com/SqlBox/SqlStatementParser'}
        ]
    }
}

