# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## Unreleased - 2019-01-01
- [Commits](https://github.com/unity-game-framework/ugf-utf8json/compare/0.0.0...0.0.0)
- [Milestone](https://github.com/unity-game-framework/ugf-utf8json/milestone/0?closed=1)

### Added
- Nothing.

### Changed
- Nothing.

### Deprecated
- Nothing.

### Removed
- Nothing.

### Fixed
- Nothing.

### Security
- Nothing.

## 3.2.2-preview - 2019-12-14
- [Commits](https://github.com/unity-game-framework/ugf-utf8json/compare/3.2.1-preview...3.2.2-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-utf8json/milestone/13?closed=1)

### Fixed
- `TypedFormatter` serialization of type information when target type does not have any serializable members.

## 3.2.1-preview - 2019-12-10
- [Commits](https://github.com/unity-game-framework/ugf-utf8json/compare/3.2.0-preview...3.2.1-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-utf8json/milestone/12?closed=1)

### Fixed
- Generated resolver results in inconsistent line endings.

## 3.2.0-preview - 2019-12-08
- [Commits](https://github.com/unity-game-framework/ugf-utf8json/compare/3.1.1-preview...3.2.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-utf8json/milestone/11?closed=1)

### Added
- `IJsonFormatterResolve.GetFormatter` to get formatter by specified type.
- `JsonFormatterBase<T>` implementation for generated formatters, now all formatters supports `JsonFormatter<object>`
- `TypedFormatter<T>` used to serialize specified value with type information.
- `ITypedFormatterTypeProvider` and `TypedFormatterTypeProvider` to manage type information required by `TypedFormatter<T>`.
- `Utf8JsonFormatterResolver` support for `TypedFormatter<T>`.
- `Utf8JsonGenerateArguments.IgnoreEmpty` to control whether to ignore formatter generation for types without any serializable members.
- `Utf8JsonResolverAssetInfo.IgnoreEmpty` to control whether to ignore formatter generation for types without any serializable members.

### Fixed
- Resolvers generation under the `Packages` folder.

## 3.1.1-preview - 2019-12-04
- [Commits](https://github.com/unity-game-framework/ugf-utf8json/compare/3.1.0-preview...3.1.1-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-utf8json/milestone/10?closed=1)

### Changed
- `Utf8JsonResolverAssetImporter.AutoGenerate` now set `false` by default.

### Fixed
- `Utf8JsonEditorUtility.FormatResolverName` to produce correct result.

## 3.1.0-preview - 2019-12-02
- [Commits](https://github.com/unity-game-framework/ugf-utf8json/compare/3.0.0-preview...3.1.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-utf8json/milestone/9?closed=1)

### Added
- `Utf8JsonResolverAssetImporter.AutoGenerate` to allow resolver generation on asset import.
- `Utf8JsonResolverAssetInfo.AutoGenerate` to control whether to re-generate resolver after depending sources have changed.

### Removed
- `Utf8JsonExternalTypeEditorUtility.IsExternalFile`: use `String.EndWith` instead.

## 3.0.0-preview - 2019-11-17
- [Commits](https://github.com/unity-game-framework/ugf-utf8json/compare/2.2.0-preview...3.0.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-utf8json/milestone/8?closed=1)

### Added
- `Utf8JsonResolverAsset` to generate resolver with additional options.

### Changed
- Package dependencies:
    - `com.ugf.code.generate`: from `3.1.0` to `4.0.0-preview`.

### Removed
- Package dependencies:
    - `com.ugf.code.assemblies`: `1.5.2`.
- Assembly dependent resolver generation.

## 2.2.0-preview - 2019-10-16
- [Commits](https://github.com/unity-game-framework/ugf-utf8json/compare/2.1.0-preview...2.2.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-utf8json/milestone/7?closed=1)

### Added
- `UnionFormatter.FormattersCount` property to get the count of added formatters.

### Changed
- Update to Unity 2019.3.

## 2.1.0-preview - 2019-09-28
- [Commits](https://github.com/unity-game-framework/ugf-utf8json/compare/2.0.0-preview.2...2.1.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-utf8json/milestone/6?closed=1)

### Added
- `UnionSerializer`: to implement custom serialization with type information.
- `UnionFormatter`: support for typeless serialization.

### Changed
- Completely rework `UnionFormatter`.

## 2.0.0-preview.2 - 2019-09-14
- [Commits](https://github.com/unity-game-framework/ugf-utf8json/compare/2.0.0-preview.1...2.0.0-preview.2)
- [Milestone](https://github.com/unity-game-framework/ugf-utf8json/milestone/5?closed=1)

### Changed
- Package dependencies:
    - `com.ugf.code.generate`: from `3.0.2` to `3.1.0`.

### Fixed
- `External Type Asset`: fixed error when generate resolver for assembly which contains infos with repeating target types.
- `External Type Generate`: fixed generate serialization for field with array return type.

## 2.0.0-preview.1 - 2019-08-18
- [Commits](https://github.com/unity-game-framework/ugf-utf8json/compare/2.0.0-preview...2.0.0-preview.1)
- [Milestone](https://github.com/unity-game-framework/ugf-utf8json/milestone/4?closed=1)

### Added
- Package dependencies:
    - `com.ugf.assemblies`: `1.5.2`.

### Changed
- Package dependencies:
    - `com.ugf.code.generate`: from `3.0.1` to `3.0.2`.
- `Formatter Property Generating`: now formatters always generate property names with camel case formatting.

### Removed
- Package dependencies:
    - `com.ugf.types`: `2.2.0`.

### Fixed
- `Utf8JsonEditorUtility`: throws exception when generating script for assembly definition without source files.

## 2.0.0-preview - 2019-08-10
- [Commits](https://github.com/unity-game-framework/ugf-utf8json/compare/1.1.0...2.0.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-utf8json/milestone/3?closed=1)

### Added
- Package short description.
- `UnionFormatter` to support polymorphism.
- Assembly definition importer menu to enable/disable and generate scripts for one or all assemblies.

### Changed
- Update to Unity 2019.2.
- Package dependencies:
    - `com.ugf.code.generate`: from `1.0.0` to `3.0.1`.
    - `com.ugf.types`: from `2.1.1` to `2.2.0`.
- Moved `Utf8Json` runtime and editor plugins from external dlls to package as source files.

### Removed
- Runtime formatters caching.
- Runtime external type defines and runtime type collecting.
- Runtime attributes to mark objects as target to generate formatter, use `SerializableAttribute` instead.

## 1.1.0 - 2019-04-30
- [Commits](https://github.com/unity-game-framework/ugf-utf8json/compare/1.0.0...1.1.0)
- [Milestone](https://github.com/unity-game-framework/ugf-utf8json/milestone/2?closed=1)

### Added
- `Utf8JsonGenerateArguments` to control formatters generation behaviour.
    - `IgnoreReadOnly` determines whether to generate formatters for the read-only fields or properties. (#5)
    - `IsTypeRequireAttribute` and `TypeRequiredAttributeShortName` to control target type to generate formatters.
- `Utf8JsonUniversalCodeGeneratorUtility` additional generate arguments support.

### Changed
- `Utf8JsonEditorUtility.GenerateFormatters` default generation behaviour has been changed.
    - All read-only fields and properties will be ignored.
    - Will generate only for those types that contains `Utf8JsonSerializable` attribute.

## 1.0.0 - 2019-04-29
- [Commits](https://github.com/unity-game-framework/ugf-utf8json/compare/2eeeb2e...1.0.0)
- [Milestone](https://github.com/unity-game-framework/ugf-utf8json/milestone/1?closed=1)

### Added
- This is a initial release.

---
> Unity Game Framework | Copyright 2019
