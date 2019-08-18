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
