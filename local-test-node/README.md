# AElf standalone testing node starting

This folder contains Dockerfile and configs for start standalone testing node with docker.

## Build

```
cd local-test-node
docker build -t aelf/standalone-testing-node .
```

### Run

```
docker run --name aelf-node --restart always \
    -itd -p 6801:6801 -p 8000:8000 -p 5001:5001 -p 5011:5011 \
    --platform linux/amd64 \
    --ulimit core=-1 \
    --security-opt seccomp=unconfined --privileged=true \
    aelf/standalone-testing-node
```

### Load keystore

Install aelf-command locally. Run the following commands to load keystore locally.
```
aelf-command load 1111111111111111111111111111111111111111111111111111111111111111
```


