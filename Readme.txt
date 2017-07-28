1.在NetworkConnection类，增加groupId字段，默认值为0；
2.在NetworkConnection类，增加SetGroupId方法，作用为给groupId字段赋值；
3.在NetworkIdentity类，增加了groupId属性，当NetworkIdentity在生成的时候，有被赋值过groupId时，则获取该groupId,否则，当有Owner的时候，返回Owner的groupID,否则返回0；
4.NetworkServer.SndWriterToReady方法增加groupID参数 ，默认为0；
5.NetworkServer.SendBytesToReady方法增加groupID参数 ，默认为0；
6.NetworkServer.SetClientReadyInternal 方法中，AddObserver时，只add groupId一致的conn
7.NetworkServer.SpawnObject方法增加groupID参数 ，默认为0；
8.NetworkServer.Spawn方法增加groupID参数 ，默认为0；
9.NetworkIdentity.RebuildObservers方法增加groupID参数，默认为0;

