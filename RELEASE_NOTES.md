# Release Notes - v2.0.1

### What's Changed

This maintenance release standardizes solution identification and improves package management.

**Updates:**

- **Solution ID Standardization**: Solution IDs now follow the new standard format using the catalog GUID as the identifier
  - This ensures consistent identification across all DataMiner solutions
  - Improves compatibility with the DataMiner Catalog integration
- **Central Package Management (CPM)**: Migrated all NuGet package references to use CPM
  - Simplifies dependency management across the solution
  - Ensures consistent package versions across all projects

---

## Version 2.0.0-rc1 (Release Candidate 1)

### What's New

This release candidate introduces a major update to help you better manage your DataMiner solutions across different environments.

**Key Improvements:**

- **Better Organization**: Easily track which solutions are installed and what version you're running
- **Centralized Registry**: All your solutions and their data types in one place
- **Browse & Review**: A user-friendly app to view all registered solutions
- **Faster Performance**: Improved speed when registering and retrieving information
- **Better Documentation**: Clear guides to help you get started quickly

### What This Means for You

When you work with multiple DataMiner solutions, it can be challenging to keep track of what's installed where. This library acts like a central catalog - it remembers what solutions you've installed, what version they are, and what types of data they include.

Think of it as an inventory system for your DataMiner environment.

### Important to Know

- This is a **test version** (Release Candidate) - please try it out in your test environment first
- Fully compatible with DataMiner 10.x and later versions
- Includes guides and examples to help you get started
- We welcome your feedback to make the final release even better

### Need Help?

See the documentation in the `docs` folder or contact the development team with questions.

---

**Remember:** Always test new versions in a safe environment before using them in production.
