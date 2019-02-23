# Assignment 6 - Due 2019-02-22

[![Build Status](https://dev.azure.com/CaseyWhite/SecretSanta/_apis/build/status/SecretSanta?branchName=Assignment6)](https://dev.azure.com/CaseyWhite/SecretSanta/_build/latest?definitionId=7&branchName=Assignment6)

## Overview
In this assignment the goal is to setup an Azure DevOps pipeline that build (compiles and runs your tests) and then deploys the code.

## Assignment

1. Create a free account in Azure.com.
2. Create a new App Service to host your website.
3. Define an organization in http://devops.azure.com and create a new project for Secret Santa.
4. Define a build pipeline that pulls the source code from your github repository, builds the code (compile and run tests).
5. Define a release pipeline that deploys the code built in #4 onto the App Service created in #2.
6. Either
  a. Share your Azure DevOps project with Mark@IntelliTect.com so that I can review your build (compile and tests) and your release pipeline. (Recommended)
  b. Saving your build page from you repository using the name BuildPage.html and your build output to BuildOutput.txt.  It should be clear from what you save that you have a valid build and build output.
7. Submit a PR to **Assignment 6** with a link to your devops.azure.com site and your deployed site.

## Notes

* It is acceptable if you have a Error 500.30 on the site you deployed.
* This assignment can be turned in at the end of day on 2/22/2019.

## Bonus Ideas

* Display an Azure DevOps badge on your github repository.
* Resolve the Error 500.30
* Run code analysis as part of your build and address all warnings appropriately
* Full Bonus awarded if you add code analysis to the solution (rather tha each individual projects) with a different ruleset for your test projects so that no warning is issued for underscores  (this bonus can be turned in separately any time before class on Thursday if you want more time). You still need to address all warnings but this can be done at the project level.
