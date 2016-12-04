#!/bin/bash

ASSEMBLIES=`find Build -type f -name Yupi.*.dll | grep -v /obj/ | grep -v Tests.dll | perl -e '@in=grep(s/\n$//, <>); print "[\"".join("\", \"", @in)."\"],\n";'`
echo "{"
echo "  \"assemblies\": ${ASSEMBLIES}" 
echo "  \"typeInclude\": \"Yupi.*\"," 
echo "  \"typeExclude\": \"Yupi.*Tests*\""
echo "}"
