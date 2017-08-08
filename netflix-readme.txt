Parallel versions of Netflix app:

1. ConcurrentDictionary:  using single, shared Concurrent Dictionary

2. FileChunk-LessStrict:  using N tasks, each processing one chunk of file
    into a local dictionary that is merged at end.  This is a "less-strict" 
    version that does not guarantee the top-10 results for pathological input
    files.  By relaxing requirements, yields a more scalable app --- and the 
    same results in this case.

3. MapReduce:          using local dictionaries in Task Local Storage,
    merging the local dictionaries at the end

4. PLINQ:              using Parallel LINQ queries

5. Producer-Consumer:  using Producer-Consumer pattern

The best performer is (2), with (3) a very close second and (4) a close third.
The common theme?  The best performers minimize/eliminate contention for 
shared resources.
