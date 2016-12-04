#!/bin/bash

set -e

[ -f "coverageKnows" ] && rm coverageKnowns
[ -f "coverageResults" ] && rm coverageResults

echo "Building tool"
xbuild Tools/SharpCover/Gaillard.SharpCover/Program.csproj &> /dev/null

echo "Generate config"

./Tools/genCovConfig.sh > ./coverageConfig.json

echo "SharpCover instrument"
mono Tools/SharpCover/Gaillard.SharpCover/bin/Debug/SharpCover.exe instrument coverageConfig.json

echo "SharpCover check"
mono Tools/SharpCover/Gaillard.SharpCover/bin/Debug/SharpCover.exe check
rm ./coverageConfig.json
