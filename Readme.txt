






//###################2017.07.31

1.增加了SpawnExDelegate(Vector3 position, NetworkHash128 assetId, string data); 以便在Spawn的时候传送其它数据
2.ClientScene增加了 SpawnExDelegate相关的RegisterPrefab,RegisterSpawnHandler方法。
3.NetworkScene增加了 存储SpawnExDelegate的相关Hashtable字段：s_SpawnExHandlers，并在获取,移除,清空Spawner时针对s_SpawnExHandlers做了相应增加删除处理，。
4.NetworkScene增加了 SpawnExDelete相关的RegisterPrefab,RegisterSpawnHandler方法。
5.NetworkScene增加了 GetSpawnHandler(NetworkHash128 assetId, out SpawnExDelegate handler)，方法用于获取新的SpawnExDelegate
6.NetworkServer增加Spawn方法的data参数，默认为空。
7.NetworkServer增加SpawnObject方法的data参数，默认为空。

//#######










1.在NetworkConnection类，增加groupId字段，默认值为0；
2.在NetworkConnection类，增加SetGroupId方法，作用为给groupId字段赋值；
3.在NetworkIdentity类，增加了groupId属性，当NetworkIdentity在生成的时候，有被赋值过groupId时，则获取该groupId,否则，当有Owner的时候，返回Owner的groupID,否则返回0；
4.NetworkServer.SndWriterToReady方法增加groupID参数 ，默认为0；
5.NetworkServer.SendBytesToReady方法增加groupID参数 ，默认为0；
6.NetworkServer.SetClientReadyInternal 方法中，AddObserver时，只add groupId一致的conn
7.NetworkServer.SpawnObject方法增加groupID参数 ，默认为0；
8.NetworkServer.Spawn方法增加groupID参数 ，默认为0；
9.NetworkIdentity.RebuildObservers方法增加groupID参数，默认为0;

