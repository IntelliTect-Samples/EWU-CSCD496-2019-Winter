# Homework 8
### Add logging to API project *
  - Microsoft.Extensions.Logging
    - You should have a private property and dependency injection to set the logger in your controllers
    - You can use Serilog
  - Have logging at multiple levels, information, error caught (informational), error fatal
  - Purely extra, you can set up a logging API that is called using providers. So then you can still do something like Logger.Log() but the Logger calls the API for you.
### Application Insights *
  - Give either a URL or a screen save (html, imgt file) of the application insights to the service
  - Azure analytics
### Add configuration [3/4]
  - Microsoft.Extensions.Configuration
    - IConfiguration / ConfigurationBuilder
    - Give a reasonable justifiable configuration of providers for the settings
      - one of them must be an environment variable
        - There is a UI in azure to configure ENV variables
      - Give a comment explaining why you have chosen what you have chosen
  - Make sure your logging is configured, so when it is on your machine it logs to the console, and when on Azure it is saved to the Sqlite DB
  - You can store configuration details in any of the following. Order of precedence top to bottom
    - Command line
    - environment variables
    - xml file / .json file
    - Database
    - source code
      - by default, if the file exists you load the config from there, if there is no file you use default hard coded config settings. You can think of this as the default developer configuration
### Add code analysis *
  - Global suppression file for test projects so _ is allowed in names (project specific)
  - Ruleset so that localization rules are turned off (solution wide)
  - **ALWAYS provide the justification for any rule suppression**
### Extra Credit
  - Configure the configuration for the entire solution with one file. Use Directory.Builds.Props file to achieve this
  - Extra extra credit, have a second ruleset for the tests that points to the primary ruleset















