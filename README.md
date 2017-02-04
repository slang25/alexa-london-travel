# Alexa London Travel

![London Travel](./static/icon-108x108.png "London Travel")

[![Build status](https://img.shields.io/travis/martincostello/alexa-london-travel/master.svg)](https://travis-ci.org/martincostello/alexa-london-travel)

[![Build history](https://buildstats.info/travisci/chart/martincostello/alexa-london-travel?branch=master&includeBuildsFromPullRequest=false)](https://travis-ci.org/martincostello/alexa-london-travel)

## Overview

An Amazon Alexa skill for checking the status for travel in London.

The skill integrates with the [TfL API](https://api.tfl.gov.uk/) to query the status of London Underground lines and the Docklands Light Railway (DLR) to tell you their current status, such as whether there are any closures or delays.

### Example Utterances

_Ask about the status of a specific tube line or for the DLR_:
> Alexa, ask London Travel about the Victoria line.

_Ask about disruption for on the London Underground or DLR:_
> Alexa, ask London Travel if there is any disruption today.

## Feedback

Any feedback or issues can be added to the issues for this project in [GitHub](https://github.com/martincostello/alexa-london-travel/issues).

## Repository

The repository is hosted in [GitHub](https://github.com/martincostello/alexa-london-travel): https://github.com/martincostello/alexa-london-travel.git

## Building and Testing

To build the skill just run [NPM](https://www.npmjs.com/) to install the packages:

```sh
> npm install
```

To run the [Mocha](https://mochajs.org/) unit tests:

```sh
> npm test
```

## Debugging

To run the skill locally using [Alexa App Server](https://github.com/alexa-js/alexa-app-server), first clone the repository into directory that is otherwise empty.

```sh
> mkdir alexa-london-travel && cd alexa-london-travel
> git clone https://github.com/martincostello/alexa-london-travel repo
> cd repo
```

Then add a ```.env``` file and populate it with your TfL API application Id and key. If you do not have TfL application Id, you can register for one [here](https://api-portal.tfl.gov.uk/docs).

```txt
TFL_APP_ID=MyAppId
TFL_APP_KEY=MyAppKey
```

Now the skill is configured, install the packages and start the test server:

```sh
> npm install
> node server
```

You can now debug the skill in a browser using the built-in test server by browsing to ```http://localhost:3001/alexa-london-travel```.

Launch files are also included in the repository to debug the skill using [Visual Studio Code](https://code.visualstudio.com/).

## License

This project is licensed under the [Apache 2.0](http://www.apache.org/licenses/LICENSE-2.0.txt) license.

## Copyright and Trademarks

The London Travel skill is copyright (&copy;) Martin Costello 2017.

Amazon Alexa is a trademark of Amazon.com, Inc.

The TfL roundel is a trademark of Transport for London (TfL).
