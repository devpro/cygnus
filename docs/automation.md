# Automation

## Platform

* [GitLab](https://gitlab.com/) is used to run the CI (Continuous Integration) & PKG (Packaging = Continuous Delivery) pipeline,
which is defined in [`.gitlab-ci.yml`](../.gitlab-ci.yml) file.

## Setup

### GitLab > CI/CD > Schedules

* Add a new schedule to run every night

### GitLab > Settings > CI/CD > Variables

* Add the following variables

Name | Value | Protected | Masked
---- | ----- | --------- | ------
NUGET_APIKEY | API key generated from nuget.org website | No | Yes
SONAR_ORGANIZATION | Sonar Organization | No | No
SONAR_PROJECTKEY | Sonar Project Key | No | No
SONAR_HOSTURL | Sonar Instance URL | No | No
SONAR_TOKEN | Sonar Key | No | Yes
