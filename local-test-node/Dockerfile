FROM aelf/node:testnet-v1.5.0

RUN apt-get update && apt-get install -y unzip

COPY appsettings.json appsettings.LocalTestNode.json /app/
COPY W1ptWN5n5mfdVvh3khTRm9KMJCAUdge9txNyVtyvZaYRYcqc1.json /root/.local/share/aelf/keys/
RUN curl https://www.nuget.org/api/v2/package/AElf.Contracts.Association/1.5.0 -L --output Association.zip && unzip Association.zip -d /root/.local/share/aelf/
RUN curl https://www.nuget.org/api/v2/package/AElf.Contracts.Configuration/1.5.0 -L --output Configuration.zip && unzip -o Configuration.zip -d /root/.local/share/aelf/
RUN curl https://www.nuget.org/api/v2/package/AElf.Contracts.Consensus.AEDPoS/1.5.0 -L --output Consensus.AEDPoS.zip && unzip -o Consensus.AEDPoS.zip -d /root/.local/share/aelf/
RUN curl https://www.nuget.org/api/v2/package/AElf.Contracts.CrossChain/1.5.0 -L --output CrossChain.zip && unzip -o CrossChain.zip -d /root/.local/share/aelf/
RUN curl https://www.nuget.org/api/v2/package/AElf.Contracts.Economic/1.5.0 -L --output Economic.zip && unzip -o Economic.zip -d /root/.local/share/aelf/
RUN curl https://www.nuget.org/api/v2/package/AElf.Contracts.Election/1.5.0 -L --output Election.zip && unzip -o Election.zip -d /root/.local/share/aelf/
RUN curl https://www.nuget.org/api/v2/package/AElf.Contracts.Genesis/1.5.0 -L --output Genesis.zip && unzip -o Genesis.zip -d /root/.local/share/aelf/
RUN curl https://www.nuget.org/api/v2/package/AElf.Contracts.MultiToken/1.5.0 -L --output MultiToken.zip && unzip -o MultiToken.zip -d /root/.local/share/aelf/
RUN curl https://www.nuget.org/api/v2/package/AElf.Contracts.NFT/1.5.0 -L --output NFT.zip && unzip -o NFT.zip -d /root/.local/share/aelf/
RUN curl https://www.nuget.org/api/v2/package/AElf.Contracts.Parliament/1.5.0 -L --output Parliament.zip && unzip -o Parliament.zip -d /root/.local/share/aelf/
RUN curl https://www.nuget.org/api/v2/package/AElf.Contracts.Profit/1.5.0 -L --output Profit.zip && unzip -o Profit.zip -d /root/.local/share/aelf/
RUN curl https://www.nuget.org/api/v2/package/AElf.Contracts.Referendum/1.5.0 -L --output Referendum.zip && unzip -o Referendum.zip -d /root/.local/share/aelf/
RUN curl https://www.nuget.org/api/v2/package/AElf.Contracts.TokenConverter/1.5.0 -L --output TokenConverter.zip && unzip -o TokenConverter.zip -d /root/.local/share/aelf/
RUN curl https://www.nuget.org/api/v2/package/AElf.Contracts.TokenHolder/1.5.0 -L --output TokenHolder.zip && unzip -o TokenHolder.zip -d /root/.local/share/aelf/
RUN curl https://www.nuget.org/api/v2/package/AElf.Contracts.Treasury/1.5.0 -L --output Treasury.zip && unzip -o Treasury.zip -d /root/.local/share/aelf/
RUN curl https://www.nuget.org/api/v2/package/AElf.Contracts.Vote/1.5.0 -L --output Vote.zip && unzip -o Vote.zip -d /root/.local/share/aelf/ 
RUN cp /root/.local/share/aelf/lib/net6.0/*.dll /app/ && rm -rf /root/.local/share/aelf/lib/
ENV ASPNETCORE_ENVIRONMENT=LocalTestNode