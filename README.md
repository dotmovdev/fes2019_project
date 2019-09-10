# fes2019_project
展示プロジェクト用のUnityプロジェクト。

[https://github.com/dotmovdev/fes2019_experimental](https://github.com/dotmovdev/fes2019_experimental)は、実験用なので、好き勝手にいじってもいいというだけ。

個人の作業の統合は、できるだけGitHubへのPushやMergeで行う。UnityPackageは、できるだけ使わない。

# Package
PackageManagerを経由してインストールしたもの。manifest.jsonにも記述されている。

|名称|バージョン|
|---|---|
|High Deginition Render Pipelines|6.9.1-preview|
|Shader Graph|6.9.1|
|Visual Effect Graph|6.9.1-preview|

# フォルダ構成
| フォルダ名 | 用途 |
|---|---|
|Resources|スクリプトから直接指定して読み込むもの。外部ソフトからインポートした3Dモデル。|
|Animations|3Dモデルなどに埋め込むアニメーション。Unityで作ったAnimatorなどは、まとめてここ。|
|Scripts|C#スクリプト。機能に応じて子フォルダを作ってもよい。|
|Material|Unityで使うマテリアル。|
|Shaders|直接コーディングしたシェーダーとShaderGraphによるシェーダー。|
|Textures|テクスチャ。用途に応じて子フォルダを作ること。|
|Prefabs|Prefabはすべてここにする。Resourcesと違う点は、スクリプトから読み込むのではなく、Inspectorで指定する点。|
|RenderPipelines|High Definition Render Pipelinesに必要なAssetと設定ファイル。|
|VFX|VisualEffectGraphで作ったものはまとめてここ。Prefab化しても、元のオブジェクトなどはここに分離する。|
