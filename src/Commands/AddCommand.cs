using System;
using System.Threading.Tasks;
using Mercurius.Modrinth;
using Mercurius.Modrinth.Models;
using Mercurius.Profiles;
using Mercurius.Configuration;

namespace Mercurius.Commands {
    public class AddCommand : BaseCommand {
        public override string Name { get => "Add"; }
        public override string Description { get => "Adds a Mod to the Selected Profile."; }
        public override string Format { get => "Add <Mod Name>"; }
        public override int ArgsQuantity => 1;
        private APIClient client;  
        private bool ignoreDependencies;
        // private List<Mod> queuedMods;
        // private List<Mod> successfullyInstalled;
        public override async Task Execute(string[] args) {
            if (args.Length < 1) throw new ArgumentException("Insuffcient Arguments Provided.");

            string query = string.Join(" ", args);
            if (args.Contains<string>("-d") || args.Contains<string>("--ignore-dependencies")) {
                ignoreDependencies = true;
                query = string.Join(" ", args.Skip(Array.IndexOf<string>(args, "-d") + 1));
            }

            if (ProfileManager.SelectedProfile == null) {
                Console.WriteLine("No profile is currently selected for install... ? (Select or create one)");
                return;
            } 

            client = new APIClient();

            await ProfileManager.AddModAsync(client, query, ignoreDependencies);

                // string id;
                // SearchModel search = await client.SearchAsync(query);
                // if (!query.ToLower().Equals(search.hits[0].title.ToLower())) {
                //     id = SelectFromList(search);
                // } else id = search.hits[0].project_id;

                // ProjectModel project = await client.GetProjectAsync(id);
                // VersionModel[] versions = await client.ListVersionsAsync(project);

                // VersionModel[] viableVersions = versions.Where<VersionModel>((version) => version.game_versions.Contains<string>(ProfileManager.SelectedProfile.MinecraftVersion)).ToArray<VersionModel>();

                // VersionModel version = await client.GetVersionInfoAsync(viableVersions[0].id);

                // Mod mod = new Mod(version, project);
                
                // if (version.dependencies.Count() > 0 && !ignoreDependencies) {
                //     Console.WriteLine("Getting Dependencies...");

                //     foreach (Dependency dependency in version.dependencies) {
                //         VersionModel dependencyVersion = await client.GetVersionInfoAsync(dependency.version_id);
                //         ProjectModel dependencyProject = await client.GetProjectAsync(dependencyVersion.project_id);

                //         Mod dependencyMod = new Mod(dependencyVersion, dependencyProject);
                //         mod.AddDependency(dependencyMod);
                //     }
                // }

            // TODO: Checks for client/server side compatibility;
            // TODO: Check for loader, and make sure mod is compatible.

            // TODO: Install should also check if mod with filename is already present before installing, and if so, just add mod to profile.  Prevents duplicate mod files. 
            // TODO: Update command
                // if (ProfileManager.SelectedProfile.Mods is not null && ProfileManager.SelectedProfile.Mods.Contains(new Mod(version, project))) {
                //     Console.WriteLine($"Mod {version.name} already installed");
                //     return;
                // }

            // Look over this again and refactor, dependency logic is a dumpster fire at best.
                // queuedMods = new List<Mod>();


                // Mod baseMod = new Mod(version, project);
                // queuedMods.Add(baseMod);

                // if (version.dependencies.Length >= 1) {
                //     foreach (Dependency dependency in version.dependencies) {
                //         VersionModel dependencyVersion = await client.GetVersionInfoAsync(dependency.version_id);
                //         ProjectModel depenencyProject = await client.GetProjectAsync(dependencyVersion.project_id);
                
                //         // await InstallDependency(dependencyVersion, depenencyProject);
                //         Mod dependencyMod = new Mod(dependencyVersion, depenencyProject);
                //         baseMod.AddDependency(dependencyMod);
                //         queuedMods.Add(dependencyMod);
                //     }
                // } 

                // if (ProfileManager.SelectedProfile.Mods.Where<Mod>(mod => mod.ProjectId == baseMod.ProjectId).Count() >= 1) {
                //     Console.WriteLine($"Mod {baseMod.Title} is already contained within this profile!");
                //     CheckForFiles();
                // } 

                // if (!(await Install(baseMod))) return;

            // Console.WriteLine("Updating Profile...");
            // await ProfileManager.SelectedProfile.UpdateModListAsync(mod);
        }
        // private async Task<bool> Install(Mod modToInstall) {
        //     if (queuedMods.Count < 1) {
        //         Console.WriteLine("There is nothing to do");
        //         return false;
        //     }

        //     Console.WriteLine("Mods Queued for Install: ");
        //     Console.WriteLine("- {0} (Base)", modToInstall.Title);

        //     foreach (Mod mod in modToInstall.Dependencies) {
        //         Console.WriteLine("- {0} (Dependency)", mod.Title);
        //     }
        //     Console.Write("\nContinue with Operation? (Y/n) ");

        //     if (Console.ReadLine().ToLower().Equals("n")) {
        //         Console.WriteLine("Aborting...");
        //         return false;
        //     }

        //     successfullyInstalled = new List<Mod>();
        //     if (!dryRun) {
        //         foreach (Mod mod in queuedMods) {
        //             if (!dryRun) await client.DownloadVersionAsync(mod);
        //             successfullyInstalled.Add(mod);
        //         }
        //     }
            
        //     return true;
        // }
        // private void CheckForFiles() {
        //     List<Mod> modsToRemove = new List<Mod>();

        //     foreach (Mod mod in queuedMods) {
        //         if (!mod.FileExists()) {
        //             Console.WriteLine("File for Mod {0} doesn't exist...?", mod.Title);
        //         } else {
        //             modsToRemove.Remove(mod);
        //         }
        //     }

        //     foreach (Mod mod in modsToRemove) {
        //         queuedMods.Remove(mod);
        //     }
        // }
    }
}