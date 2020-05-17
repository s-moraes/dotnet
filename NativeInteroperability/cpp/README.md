# Configure the MakeFile
cmake .
# Build with all cores available (Mac OS)
make -j$(sysctl hw.ncpu | cut -d: -f2)
# Build with all cores available (Linux)
make -j$(grep -c ^processor /proc/cpuinfo)

